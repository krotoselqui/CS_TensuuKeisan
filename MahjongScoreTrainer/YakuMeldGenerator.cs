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
                if (RandomSelection.RetOne(kanRate) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(39, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(39, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(39, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
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
                if (RandomSelection.RetOne(10) == 1)
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

        internal static void GenerateSuuankouTanki(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RandomSelection.RetOne(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(41, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(41, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
            }
            MeldGenerator.MakeRandomAtama(41, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = parts[4][1];
        }

        internal static void GenerateTsuiisou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RandomSelection.RetOne(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(42, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(42, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }

            MeldGenerator.MakeRandomAtama(42, ref remaining, out unused, out generated, 3);
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

        internal static void GenerateRyuuiisou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            if (RandomSelection.RetOne(10) == 1)
            {
                MeldGenerator.MakeRandomKantsu(43, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(43, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = 1 + RandomSelection.RetOne(openRate);
            }
            if (RandomSelection.RetOne(2) == 1)
            {
                MeldGenerator.MakeRandomShuntsu(43, ref remaining, out unused, out generated, 2, 2);
                parts[1] = generated;
                partTypes[1] = 5 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(43, ref remaining, out unused, out generated, 2);
                parts[1] = generated;
                partTypes[1] = 1 + RandomSelection.RetOne(openRate);
            }
            for (int i = 2; i < 4; i++)
            {
                if (RandomSelection.RetOne(20) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(43, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(43, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(43, ref remaining, out unused, out generated, 2);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateChinroutou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(44, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(44, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(44, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateDaisuushi(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RandomSelection.RetOne(10) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(49, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(49, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(49, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateShousuushi(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            int pairWind = Program.r.Next(4) + 1;
            int[] winds = new int[3];
            int index = 0;
            for (int wind = 1; wind <= 4; wind++)
            {
                if (wind != pairWind) winds[index++] = wind;
            }

            string unused;
            int[] generated;
            for (int i = 0; i < 3; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(50, ref remaining, out unused, out generated, 3, winds[i]);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(50, ref remaining, out unused, out generated, 3, winds[i]);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }

            int kind = Program.r.Next(20);
            int color = Program.r.Next(3);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(50, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = 2;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(50, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = 4;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(50, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(50, ref remaining, out unused, out generated, 3, pairWind);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateSuukantsu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            // Retain the legacy random draw even though the value is not otherwise used.
            Program.r.Next(7);
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                MeldGenerator.MakeRandomKantsu(51, ref remaining, out unused, out generated);
                parts[i] = generated;
                partTypes[i] = 3 + RandomSelection.RetOne(openRate);
            }
            MeldGenerator.MakeRandomAtama(51, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateIttsu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            int color = Program.r.Next(3);
            int[] starts = { 1, 4, 7 };
            for (int i = 0; i < starts.Length; i++)
            {
                MeldGenerator.MakeRandomShuntsu(24, ref remaining, out unused, out generated, color, starts[i]);
                parts[i] = generated;
                partTypes[i] = 5 + Program.r.Next(2);
            }
            int kind = Program.r.Next(9);
            int open = Program.r.Next(4);
            open = open == 0 ? 1 : 0;
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(24, ref remaining, out unused, out generated);
                partTypes[3] = 1 + open;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(24, ref remaining, out unused, out generated);
                partTypes[3] = 3 + open;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(24, ref remaining, out unused, out generated);
                partTypes[3] = 5 + open;
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(24, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateSanshokuDoujun(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 10;
            string unused;
            int[] generated;
            int start = Program.r.Next(7) + 1;
            for (int color = 0; color < 3; color++)
            {
                MeldGenerator.MakeRandomShuntsu(25, ref remaining, out unused, out generated, color, start);
                parts[color] = generated;
                partTypes[color] = 5 + RandomSelection.RetOne(openRate);
            }
            int kind = Program.r.Next(9);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(25, ref remaining, out unused, out generated);
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(25, ref remaining, out unused, out generated);
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(25, ref remaining, out unused, out generated);
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(25, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateHonchantaiyaochuu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                int kind = Program.r.Next(9);
                int open = Program.r.Next(2);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(23, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(23, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(23, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(23, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(23, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateYakuhai(int yakuNumber, ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            int honor = yakuNumber - 13;
            string unused;
            int[] generated;
            int kind = Program.r.Next(2);
            int open = Program.r.Next(2);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKantsu(14, ref remaining, out unused, out generated, 3, honor);
                partTypes[0] = 3 + open;
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(14, ref remaining, out unused, out generated, 3, honor);
                partTypes[0] = 1 + open;
            }
            parts[0] = generated;
            for (int i = 1; i < 4; i++)
            {
                kind = Program.r.Next(9);
                open = Program.r.Next(2);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(14, ref remaining, out unused, out generated);
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(14, ref remaining, out unused, out generated);
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(14, ref remaining, out unused, out generated);
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(14, ref remaining, out unused, out generated);
                    partTypes[i] = 5;
                }
                parts[i] = generated;
            }
            MeldGenerator.MakeRandomAtama(14, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateIipeikou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            MeldGenerator.MakeRandomShuntsu(9, ref remaining, out unused, out generated);
            parts[0] = generated;
            partTypes[0] = 5;
            int color = TileUtilities.GetColor(generated[0]);
            int number = TileUtilities.GetNumber(generated[0]);
            MeldGenerator.MakeRandomShuntsu(9, ref remaining, out unused, out generated, color, number);
            parts[1] = generated;
            partTypes[1] = 5;
            for (int i = 2; i < 4; i++)
            {
                int kind = Program.r.Next(9);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(9, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(9, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(9, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(8, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateTanyao(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                int kind = Program.r.Next(9);
                int open = Program.r.Next(2);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(8, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(8, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(8, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(8, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(8, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GeneratePinfu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                MeldGenerator.MakeRandomShuntsu(-1, ref remaining, out unused, out generated);
                parts[i] = generated;
                partTypes[i] = 5;
            }
            MeldGenerator.MakeRandomAtama(7, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = TileUtilities.GetNumber(parts[0][0]) == 7 ? parts[0][2] : parts[0][0];
        }
    }
}
