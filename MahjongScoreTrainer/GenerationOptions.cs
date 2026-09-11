using System;

namespace MahjongScoreTrainer
{
    sealed class GenerationOptions
    {
        public int ProblemCount { get; private set; }
        public int Seed { get; private set; }
        public int DisplayMode { get; private set; }

        public GenerationOptions(int problemCount, int seed, int displayMode)
        {
            if (problemCount < 1) throw new ArgumentOutOfRangeException("problemCount");
            if (seed < -1) throw new ArgumentOutOfRangeException("seed");
            if (displayMode != 0 && displayMode != 1) throw new ArgumentOutOfRangeException("displayMode");
            ProblemCount = problemCount;
            Seed = seed;
            DisplayMode = displayMode;
        }
    }
}
