# Manual provenance tool. CI compares fixtures; it never regenerates them.
param([string]$Revision = '50eb05d')
$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path $PSScriptRoot -Parent
$scratch = Join-Path $env:TEMP ('mahjong-baseline-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $scratch | Out-Null
$utf8 = New-Object Text.UTF8Encoding($false)
$paths = @('Program.cs', 'TileUtilities.cs', 'HandEvaluator.cs', 'WinningHandData.cs',
    'HandDecomposition.cs', 'ScoreResult.cs', 'YakuDefinition.cs')
foreach ($path in $paths) {
    $source = & git -c "safe.directory=$($repoRoot.Replace('\', '/'))" -C $repoRoot show "${Revision}:MahjongScoreTrainer/$path"
    if ($LASTEXITCODE -ne 0) { throw "Cannot read $Revision/$path" }
    $text = $source -join "`r`n"
    if ($path -eq 'Program.cs') { $text = "using Console = BaselineConsole;`r`n" + $text }
    [IO.File]::WriteAllText((Join-Path $scratch $path), $text, $utf8)
}
$adapter = @'
using System;
using System.IO;
using System.Reflection;
using System.Text;
static class BaselineConsole
{
    internal static TextReader Input;
    internal static TextWriter Output;
    public static int CursorLeft { get; set; }
    public static int CursorTop { get; set; }
    public static void SetCursorPosition(int left, int top) { }
    public static void Write(string value) { Output.Write(value); }
    public static void WriteLine(string value) { Output.WriteLine(value); }
    public static void WriteLine() { Output.WriteLine(); }
    public static string ReadLine() { return Input.ReadLine(); }
    public static ConsoleKeyInfo ReadKey() { return new ConsoleKeyInfo(); }
    public static void Main(string[] args)
    {
        Input = new StringReader(args[0] + "\n" + args[1] + "\n" + args[2] + "\n");
        using (Output = new StreamWriter("console.txt", false, new UTF8Encoding(false)))
        {
            typeof(MahjongScoreTrainer.Program).GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { new string[0] });
        }
    }
}
'@
[IO.File]::WriteAllText((Join-Path $scratch 'BaselineConsole.cs'), $adapter, $utf8)
$compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$sources = @(Get-ChildItem $scratch -Filter '*.cs' | ForEach-Object { $_.FullName })
& $compiler /nologo /target:exe /main:BaselineConsole "/out:$scratch/baseline.exe" @sources
if ($LASTEXITCODE -ne 0) { throw 'Baseline compilation failed.' }
foreach ($seed in @(0, 12345)) {
    foreach ($mode in @(0, 1)) {
        $output = Join-Path $repoRoot "tests/Fixtures/seed-$seed-mode-$mode"
        if (Test-Path $output) { throw "Refusing to overwrite baseline: $output" }
        New-Item -ItemType Directory -Path $output | Out-Null
        Push-Location $output
        try {
            & (Join-Path $scratch 'baseline.exe') 20 $seed $mode
            if ($LASTEXITCODE -ne 0) { throw 'Baseline execution failed.' }
        }
        finally { Pop-Location }
    }
}
Write-Host "Captured original Main from $Revision. Temporary compiler files: $scratch"
