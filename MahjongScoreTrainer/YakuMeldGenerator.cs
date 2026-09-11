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
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
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
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = 1 + RandomSelection.RetOne(openRate);
            }
            if (RandomSelection.RetOne(2) == 1)
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2, 2);
                parts[1] = generated;
                partTypes[1] = 5 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                parts[1] = generated;
                partTypes[1] = 1 + RandomSelection.RetOne(openRate);
            }
            for (int i = 2; i < 4; i++)
            {
                if (RandomSelection.RetOne(20) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
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
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Chinroutou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Chinroutou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Chinroutou, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisuushi, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisuushi, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Daisuushi, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, 3, winds[i]);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, 3, winds[i]);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }

            int kind = Program.r.Next(20);
            int color = Program.r.Next(3);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = 2;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = 4;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, 3, pairWind);
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
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Suukantsu, ref remaining, out unused, out generated);
                parts[i] = generated;
                partTypes[i] = 3 + RandomSelection.RetOne(openRate);
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Suukantsu, ref remaining, out unused, out generated);
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
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated, color, starts[i]);
                parts[i] = generated;
                partTypes[i] = 5 + Program.r.Next(2);
            }
            int kind = Program.r.Next(9);
            int open = Program.r.Next(4);
            open = open == 0 ? 1 : 0;
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = 1 + open;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = 3 + open;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = 5 + open;
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateSanshokuDoukou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 10;
            string unused;
            int[] generated;
            int number = Program.r.Next(7) + 1;
            for (int color = 0; color < 3; color++)
            {
                if (RandomSelection.RetOne(10) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated, color, number);
                    partTypes[color] = 1 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated, color, number);
                    partTypes[color] = 3 + RandomSelection.RetOne(openRate);
                }
                parts[color] = generated;
            }
            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
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
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated, color, start);
                parts[color] = generated;
                partTypes[color] = 5 + RandomSelection.RetOne(openRate);
            }
            int kind = Program.r.Next(9);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
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
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated, 3, honor);
                partTypes[0] = 3 + open;
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated, 3, honor);
                partTypes[0] = 1 + open;
            }
            parts[0] = generated;
            for (int i = 1; i < 4; i++)
            {
                kind = Program.r.Next(9);
                open = Program.r.Next(2);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = 5;
                }
                parts[i] = generated;
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
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
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
            parts[0] = generated;
            partTypes[0] = 5;
            int color = TileUtilities.GetColor(generated[0]);
            int number = TileUtilities.GetNumber(generated[0]);
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated, color, number);
            parts[1] = generated;
            partTypes[1] = 5;
            for (int i = 2; i < 4; i++)
            {
                int kind = Program.r.Next(9);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
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
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + open;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + open;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5 + open;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 5;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateSanankou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            Program.r.Next(7); // preserve the original unused sansyokunum draw
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 3; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1;
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 2;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 4;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int partIndex = 4 - i;
                if (partTypes[partIndex] == 0) winningTile = parts[partIndex][1];
            }
        }

        internal static void GenerateToitoi(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Toitoi, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 3 + RandomSelection.RetOne(openRate);
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Toitoi, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = 1 + RandomSelection.RetOne(openRate);
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Toitoi, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == 0 || type == 1 || type == 5)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateSankantsu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            // Keep the original draw for the unused sansyokunum variable.
            Program.r.Next(7);
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 3; i++)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[i] = generated;
                partTypes[i] = 3 + RandomSelection.RetOne(openRate);
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 1 + RandomSelection.RetOne(openRate);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 3 + RandomSelection.RetOne(openRate);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = 5 + RandomSelection.RetOne(openRate);
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
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
            MeldGenerator.MakeRandomAtama(YakuNumbers.Pinfu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = 0;
            winningTile = TileUtilities.GetNumber(parts[0][0]) == 7 ? parts[0][2] : parts[0][0];
        }
    }
}
