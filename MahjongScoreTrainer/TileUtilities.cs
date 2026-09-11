using System;
namespace MahjongScoreTrainer
{
    static class TileUtilities
    {
        public static int GetColor(int tileType)
        {
            int i = -1;
            if ((tileType > 34) || (tileType <= 0)) return -1;
            i = (int)((tileType - 1) / 9);
            return i;
        }

        public static int GetNumber(int tileType)
        {
            int i = -1;
            if ((tileType > 34) || (tileType <= 0)) return -1;
            i = ((tileType - 1) % 9) + 1;
            return i;
        }

        public static bool IsTerminalOrHonor(int tileType)
        {
            if (GetColor(tileType) == 3) return true;
            return GetNumber(tileType) == 1 || GetNumber(tileType) == 9;
        }
    }
}
