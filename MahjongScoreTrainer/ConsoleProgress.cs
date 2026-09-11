using System;

namespace MahjongScoreTrainer
{
    sealed class ConsoleProgress
    {
        private int currentBar;
        private int left;
        private int top;
        private const int MaxBar = 20;

        public ConsoleProgress()
        {
            Console.WriteLine("");
            Console.WriteLine("                 |Start             |Fin");
            Console.Write("Processing...    ");
            left = Console.CursorLeft;
            top = Console.CursorTop;
        }

        public void Report(int index, int problemCount)
        {
            Console.SetCursorPosition(left, top);
            while ((index * 100 / problemCount) >= (currentBar * 100 / MaxBar))
            {
                Console.Write("|");
                currentBar++;
            }
            left = Console.CursorLeft;
            top = Console.CursorTop;
            Console.SetCursorPosition(left, top);
            Console.Write(" " + (index + 1).ToString() + " / " + problemCount.ToString());
            Console.Write("   +｡ﾟφ(ゝω・｀ )+｡ﾟ ｶｷｶｷ");
        }
    }
}
