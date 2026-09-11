using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MahjongScoreTrainer;

// Only console access is substituted in a separate build of the application sources.
// Main, generation, scoring, file opening and encoding run unchanged.
static class TestConsole
{
    internal static TextReader Input;
    internal static TextWriter Output;
    internal static int WaitCount;
    public static TextReader In { get { return Input; } }
    public static TextWriter Out { get { return Output; } }
    public static int CursorLeft { get; set; }
    public static int CursorTop { get; set; }
    public static void SetCursorPosition(int left, int top) { }
    public static void Write(string value) { Output.Write(value); }
    public static void WriteLine(string value) { Output.WriteLine(value); }
    public static void WriteLine() { Output.WriteLine(); }
    public static string ReadLine() { return Input.ReadLine(); }
    public static ConsoleKeyInfo ReadKey() { WaitCount++; return new ConsoleKeyInfo(); }
}

static class GenerationChecks
{
    private static readonly string[] FileNames = { "question.txt", "answer.txt", "answer_yaku.txt" };
    private static int assertions;

    private static void Check(bool condition, string message)
    {
        assertions++;
        if (!condition) throw new Exception(message);
    }

    private sealed class ProbeWriter : StringWriter
    {
        internal bool IsDisposed;
        internal bool Fail;
        public override void WriteLine(string value)
        {
            if (Fail) throw new IOException("Expected test failure");
            base.WriteLine(value);
        }
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }

    public static void Main(string[] args)
    {
        string fixtures = args[0];
        string scratch = args[1];
        string originalDirectory = Environment.CurrentDirectory;
        try
        {
            foreach (int seed in new[] { 0, 12345 })
            {
                foreach (int mode in new[] { 0, 1 })
                {
                    string name = "seed-" + seed + "-mode-" + mode;
                    string baseline = Path.Combine(fixtures, name);
                    string output = Path.Combine(scratch, name);
                    Directory.CreateDirectory(output);
                    Environment.CurrentDirectory = output;
                    TestConsole.Input = new StringReader("20\n" + seed + "\n" + mode + "\n");
                    TestConsole.Output = new StringWriter();
                    TestConsole.WaitCount = 0;
                    typeof(Program).GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic)
                        .Invoke(null, new object[] { new string[0] });
                    foreach (string file in FileNames)
                    {
                        Check(File.ReadAllBytes(Path.Combine(baseline, file)).SequenceEqual(File.ReadAllBytes(file)), name + " Main " + file);
                        // Writers owned by Main must be closed when it returns.
                        using (File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
                    }
                    Check(TestConsole.Output.ToString() == File.ReadAllText(Path.Combine(baseline, "console.txt"), Encoding.UTF8), name + " console and summary");
                    Check(TestConsole.WaitCount == 1, "Main must wait once");
                    Check(File.ReadAllLines("question.txt", Encoding.GetEncoding(932)).Length == 20, "question count");
                    Check(File.ReadAllLines("answer_yaku.txt", Encoding.GetEncoding(932)).Length == 100, "four conditions per question");

                    // A second run in the same process must reset counts, mode and RNG binding.
                    var questions = new ProbeWriter();
                    var answers = new ProbeWriter();
                    var details = new ProbeWriter();
                    Random oldRandom = Program.r;
                    int oldMode = Program.PaiDisp_Mode;
                    int progressCount = 0;
                    int[] counts = Program.GenerateProblems(new GenerationOptions(20, seed, mode), new Random(seed),
                        questions, answers, details, (index, total) =>
                        {
                            Check(index == progressCount && total == 20, "progress order");
                            progressCount++;
                        });
                    Check(progressCount == 20, "progress count");
                    Check(ReferenceEquals(oldRandom, Program.r) && oldMode == Program.PaiDisp_Mode, "state restored after success");
                    Check(!questions.IsDisposed && !answers.IsDisposed && !details.IsDisposed, "borrowed writers stay open");
                    string[] texts = { questions.ToString(), answers.ToString(), details.ToString() };
                    for (int i = 0; i < texts.Length; i++)
                        Check(Encoding.GetEncoding(932).GetBytes(texts[i]).SequenceEqual(File.ReadAllBytes(FileNames[i])), "repeated generation " + FileNames[i]);
                    int[] expectedCounts = File.ReadAllLines(Path.Combine(baseline, "console.txt"), Encoding.UTF8)
                        .Where(line => line.Contains(" : ") && line.EndsWith(" 回"))
                        .Select(line => int.Parse(line.Split(new[] { " : " }, StringSplitOptions.None)[1].Split(' ')[0])).ToArray();
                    Check(counts.SequenceEqual(expectedCounts), "repeated summary counts");
                }
            }

            var prompts = new StringWriter();
            GenerationOptions options = Program.ReadOptions(new StringReader("bad\n0\n2147483648\n1\n-2\n-1\n2\n0\n"), prompts);
            Check(options.ProblemCount == 1 && options.Seed == -1 && options.DisplayMode == 0, "invalid input retries and boundary values");
            Check(prompts.ToString().Split(new[] { "問題数を入力 : " }, StringSplitOptions.None).Length - 1 == 4, "question prompt repeats");

            Random savedRandom = Program.r;
            int savedMode = Program.PaiDisp_Mode;
            var failureWriter = new ProbeWriter { Fail = true };
            bool failed = false;
            try
            {
                Program.GenerateProblems(new GenerationOptions(1, 0, 0), new Random(0), failureWriter, new StringWriter(), new StringWriter());
            }
            catch (IOException) { failed = true; }
            Check(failed, "I/O exception is not swallowed");
            Check(ReferenceEquals(savedRandom, Program.r) && savedMode == Program.PaiDisp_Mode, "state restored after failure");
            Check(!failureWriter.IsDisposed, "borrowed writer retained after failure");

            // If opening the second output fails, Main must close the first one.
            string lockedOutput = Path.Combine(scratch, "locked-output");
            Directory.CreateDirectory(lockedOutput);
            Environment.CurrentDirectory = lockedOutput;
            TestConsole.Input = new StringReader("1\n0\n1\n");
            TestConsole.Output = new StringWriter();
            TestConsole.WaitCount = 0;
            failed = false;
            using (File.Open("answer.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                try
                {
                    typeof(Program).GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic)
                        .Invoke(null, new object[] { new string[0] });
                }
                catch (TargetInvocationException error)
                {
                    if (!(error.InnerException is IOException)) throw;
                    failed = true;
                }
            }
            Check(failed && TestConsole.WaitCount == 0, "failed output creation must not report normal completion");
            using (File.Open("question.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
        }
        finally { Environment.CurrentDirectory = originalDirectory; }
        Console.WriteLine("PASS: Main fixtures, repeated generation, progress, input and failures (" + assertions + " assertions).");
    }
}
