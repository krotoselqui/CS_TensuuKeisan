using System;

namespace MahjongScoreTrainer
{
    internal static class YakuMeldGenerator
    {
        internal static void GenerateDaisangen(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int kanRate = 10;
            const int openRate = 2;
            string unused;
            int[] generated;

            for (int i = 0; i < 3; i++)
            {
                if (RetOne(kanRate) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(39, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 1 + RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(39, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 3 + RetOne(openRate);
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 1 + RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 3 + RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(39, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;

            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5))
                {
                    winningTile = parts[i][1];
                }
            }
        }

        internal static void GenerateSuuankou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RetOne(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(40, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(40, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
            }

            MeldGenerator.MakeRandomAtama(40, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5))
                {
                    winningTile = parts[i][1];
                }
            }
        }

        private static int RetOne(int divisor)
        {
            if (divisor < 0) return 0;
            return Program.r.Next(divisor) == 0 ? 1 : 0;
        }
    }
}
