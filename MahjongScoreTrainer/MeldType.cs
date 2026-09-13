namespace MahjongScoreTrainer
{
    internal static class MeldType
    {
        internal const int Pair = 0;
        internal const int ConcealedTriplet = 1;
        internal const int OpenTriplet = 2;
        internal const int ConcealedQuad = 3;
        internal const int OpenQuad = 4;
        internal const int ConcealedSequence = 5;
        internal const int OpenSequence = 6;

        internal static int ApplyExposure(int concealedType, int exposureFlag)
        {
            if (exposureFlag == 0) return concealedType;
            switch (concealedType)
            {
                case ConcealedTriplet: return OpenTriplet;
                case ConcealedQuad: return OpenQuad;
                case ConcealedSequence: return OpenSequence;
                default: return concealedType;
            }
        }
    }
}
