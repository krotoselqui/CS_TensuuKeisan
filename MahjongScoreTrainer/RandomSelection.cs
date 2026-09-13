namespace MahjongScoreTrainer
{
    internal static class RandomSelection
    {
        internal static int OneIn(int divisor)
        {
            if (divisor < 0) return 0;
            return Program.r.Next(divisor) == 0 ? 1 : 0;
        }
    }
}
