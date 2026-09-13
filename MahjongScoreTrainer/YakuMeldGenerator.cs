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
                if (RandomSelection.OneIn(kanRate) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated, 3, i + 5);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Daisangen, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;

            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence))
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
                if (RandomSelection.OneIn(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedQuad;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedTriplet;
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Suuankou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence))
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
                if (RandomSelection.OneIn(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedQuad;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedTriplet;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.SuuankouTanki, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = parts[4][1];
        }

        internal static void GenerateTsuiisou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RandomSelection.OneIn(10) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Tsuiisou, ref remaining, out unused, out generated, 3);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence))
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
            if (RandomSelection.OneIn(10) == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 3, 6);
                parts[0] = generated;
                partTypes[0] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            if (RandomSelection.OneIn(2) == 1)
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2, 2);
                parts[1] = generated;
                partTypes[1] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                parts[1] = generated;
                partTypes[1] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            for (int i = 2; i < 4; i++)
            {
                if (RandomSelection.OneIn(20) == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Ryuuiisou, ref remaining, out unused, out generated, 2);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Chinroutou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Chinroutou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateDaisuushi(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (RandomSelection.OneIn(10) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Daisuushi, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Daisuushi, ref remaining, out unused, out generated, 3, i + 1);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Daisuushi, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, 3, winds[i]);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }

            int kind = Program.r.Next(20);
            int color = Program.r.Next(3);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = MeldType.OpenTriplet;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, color);
                parts[3] = generated;
                partTypes[3] = MeldType.OpenQuad;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Shousuushi, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Shousuushi, ref remaining, out unused, out generated, 3, pairWind);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Suukantsu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, Program.r.Next(2));
            }
            int kind = Program.r.Next(9);
            int open = Program.r.Next(4);
            open = open == 0 ? 1 : 0;
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, open);
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedQuad, open);
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, open);
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.Ittsu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                if (RandomSelection.OneIn(10) == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated, color, number);
                    partTypes[color] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated, color, number);
                    partTypes[color] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                parts[color] = generated;
            }
            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.SanshokuDoukou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[color] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }
            int kind = Program.r.Next(9);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }
            parts[3] = generated;
            MeldGenerator.MakeRandomAtama(YakuNumbers.SanshokuDoujun, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, open);
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, open);
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, open);
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedSequence;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Honchantaiyaochuu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[0] = MeldType.ApplyExposure(MeldType.ConcealedQuad, open);
            }
            else
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated, 3, honor);
                partTypes[0] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, open);
            }
            parts[0] = generated;
            for (int i = 1; i < 4; i++)
            {
                kind = Program.r.Next(9);
                open = Program.r.Next(2);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, open);
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, open);
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, open);
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ConcealedSequence;
                }
                parts[i] = generated;
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Yakuhai, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateIipeikou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
            parts[0] = generated;
            partTypes[0] = MeldType.ConcealedSequence;
            int color = TileUtilities.GetColor(generated[0]);
            int number = TileUtilities.GetNumber(generated[0]);
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated, color, number);
            parts[1] = generated;
            partTypes[1] = MeldType.ConcealedSequence;
            for (int i = 2; i < 4; i++)
            {
                int kind = Program.r.Next(9);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedTriplet;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedQuad;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Iipeikou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedSequence;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, open);
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, open);
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, open);
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedSequence;
                }
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Tanyao, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateHonitsu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            int selectedColor = Program.r.Next(3);
            int kanCount = 0;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                int kind = Program.r.Next(4);
                int color = selectedColor;
                if (Program.r.Next(2) == 1) color = 3;
                const int openRate = 2;
                if (color != 3 && kind == 1 && kanCount >= 2) kind = 5;
                if (color != 3 && kind == 1 && i == 3) kind = 5;
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Honitsu, ref remaining, out unused, out generated, color);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Honitsu, ref remaining, out unused, out generated, color);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                    kanCount++;
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honitsu, ref remaining, out unused, out generated, selectedColor);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Honitsu, ref remaining, out unused, out generated, selectedColor);
                    partTypes[i] = MeldType.ConcealedSequence;
                }
                parts[i] = generated;
            }
            // Preserve the original constraint identifier used for this pair.
            MeldGenerator.MakeRandomAtama(YakuNumbers.Tanyao, ref remaining, out unused, out generated, selectedColor);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateJunchantaiyaochuu(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 3;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                int kind = Program.r.Next(12);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Junchantaiyaochuu, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Junchantaiyaochuu, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else if (kind == 2)
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Junchantaiyaochuu, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Junchantaiyaochuu, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ConcealedSequence;
                }
                parts[i] = generated;
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Junchantaiyaochuu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateRyanpeikou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            string unused;
            int[] generated;
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryanpeikou, ref remaining, out unused, out generated);
            parts[0] = generated;
            partTypes[0] = MeldType.ConcealedSequence;
            int color = TileUtilities.GetColor(generated[0]);
            int number = TileUtilities.GetNumber(generated[0]);
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryanpeikou, ref remaining, out unused, out generated, color, number);
            parts[1] = generated;
            partTypes[1] = MeldType.ConcealedSequence;

            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryanpeikou, ref remaining, out unused, out generated);
            parts[2] = generated;
            partTypes[2] = MeldType.ConcealedSequence;
            color = TileUtilities.GetColor(generated[0]);
            number = TileUtilities.GetNumber(generated[0]);
            MeldGenerator.MakeRandomShuntsu(YakuNumbers.Ryanpeikou, ref remaining, out unused, out generated, color, number);
            parts[3] = generated;
            partTypes[3] = MeldType.ConcealedSequence;

            MeldGenerator.MakeRandomAtama(YakuNumbers.Ryanpeikou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateHonroutou(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            const int openRate = 2;
            string unused;
            int[] generated;
            for (int i = 0; i < 4; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Honroutou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Honroutou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Honroutou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
            }
        }

        internal static void GenerateShousangen(ref int[] remaining, int[][] parts, int[] partTypes, ref int winningTile)
        {
            int pairHonor = Program.r.Next(3) + 5;
            int[] tripletHonors = new int[2];
            const int openRate = 2;
            if (pairHonor == 5) { tripletHonors[0] = 6; tripletHonors[1] = 7; }
            else if (pairHonor == 6) { tripletHonors[0] = 5; tripletHonors[1] = 7; }
            else { tripletHonors[0] = 5; tripletHonors[1] = 6; }

            string unused;
            int[] generated;
            for (int i = 0; i < 2; i++)
            {
                if (Program.r.Next(20) == 0)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Shousangen, ref remaining, out unused, out generated, 3, tripletHonors[i]);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousangen, ref remaining, out unused, out generated, 3, tripletHonors[i]);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
                parts[i] = generated;
            }

            for (int i = 2; i < 4; i++)
            {
                int kind = Program.r.Next(20);
                int color = Program.r.Next(3);
                if (kind == 0)
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Shousangen, ref remaining, out unused, out generated, color);
                    partTypes[i] = MeldType.OpenTriplet;
                }
                else if (kind == 1)
                {
                    MeldGenerator.MakeRandomKantsu(YakuNumbers.Shousangen, ref remaining, out unused, out generated, color);
                    partTypes[i] = MeldType.OpenQuad;
                }
                else
                {
                    MeldGenerator.MakeRandomShuntsu(YakuNumbers.Shousangen, ref remaining, out unused, out generated);
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
                }
                parts[i] = generated;
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Shousangen, ref remaining, out unused, out generated, 3, pairHonor);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                    partTypes[i] = MeldType.ConcealedQuad;
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ConcealedTriplet;
                }
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.OpenTriplet;
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.OpenQuad;
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Sanankou, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = -1;
            for (int i = 0; i < 5; i++)
            {
                int partIndex = 4 - i;
                if (partTypes[partIndex] == MeldType.Pair) winningTile = parts[partIndex][1];
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
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
                }
                else
                {
                    MeldGenerator.MakeRandomKotsu(YakuNumbers.Toitoi, ref remaining, out unused, out generated);
                    parts[i] = generated;
                    partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
                }
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Toitoi, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[i] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }

            int kind = Program.r.Next(20);
            if (kind == 0)
            {
                MeldGenerator.MakeRandomKotsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedTriplet, RandomSelection.OneIn(openRate));
            }
            else if (kind == 1)
            {
                MeldGenerator.MakeRandomKantsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedQuad, RandomSelection.OneIn(openRate));
            }
            else
            {
                MeldGenerator.MakeRandomShuntsu(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
                parts[3] = generated;
                partTypes[3] = MeldType.ApplyExposure(MeldType.ConcealedSequence, RandomSelection.OneIn(openRate));
            }

            MeldGenerator.MakeRandomAtama(YakuNumbers.Sankantsu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            for (int i = 0; i < 5; i++)
            {
                int type = partTypes[i];
                if (winningTile == -1 && (type == MeldType.Pair || type == MeldType.ConcealedTriplet || type == MeldType.ConcealedSequence)) winningTile = parts[i][1];
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
                partTypes[i] = MeldType.ConcealedSequence;
            }
            MeldGenerator.MakeRandomAtama(YakuNumbers.Pinfu, ref remaining, out unused, out generated);
            parts[4] = generated;
            partTypes[4] = MeldType.Pair;
            winningTile = TileUtilities.GetNumber(parts[0][0]) == 7 ? parts[0][2] : parts[0][0];
        }
    }
}
