using System;

namespace MahjongScoreTrainer
{
    internal static class SpecialHandGenerator
    {
        internal static void Generate(int yakuNum, ref int[] remaining, ref int[] hand, ref int winningTile)
        {
            if (yakuNum == YakuNumbers.Chiitoitsu)
            {
                int position = 0;
                int waitingPair = Program.r.Next(7);
                for (int i = 0; i < remaining.Length; i++) remaining[i] -= 2;
                for (int i = 0; i < 7; i++)
                {
                    string unused;
                    int[] pair;
                    MeldGenerator.MakeRandomAtama(YakuNumbers.Chiitoitsu, ref remaining, out unused, out pair);
                    hand[position] = pair[0];
                    hand[position + 1] = pair[1];
                    if (waitingPair == i) winningTile = hand[position];
                    position += 2;
                }
                return;
            }

            if (yakuNum == YakuNumbers.ChuurenPoutou || yakuNum == YakuNumbers.JunseiChuurenPoutou)
            {
                int color = Program.r.Next(3);
                int position = 0;
                for (int i = 0; i < 3; i++) hand[position++] = color * 9 + 1;
                for (int i = 2; i <= 8; i++) hand[position++] = color * 9 + i;
                for (int i = 0; i < 3; i++) hand[position++] = color * 9 + 9;
                int added = color * 9 + Program.r.Next(9) + 1;
                hand[position] = added;
                winningTile = added;
                if (yakuNum == YakuNumbers.ChuurenPoutou)
                {
                    while (added == winningTile) winningTile = color * 9 + Program.r.Next(9) + 1;
                }
                return;
            }

            int[] terminalsAndHonors = { 1, 9, 10, 18, 19, 27, 28, 29, 30, 31, 32, 33, 34 };
            for (int i = 0; i < terminalsAndHonors.Length; i++) hand[i] = terminalsAndHonors[i];
            int addedTile = terminalsAndHonors[Program.r.Next(terminalsAndHonors.Length)];
            hand[13] = addedTile;
            winningTile = addedTile;
            if (yakuNum == YakuNumbers.KokushiMusou)
            {
                while (addedTile == winningTile) winningTile = terminalsAndHonors[Program.r.Next(terminalsAndHonors.Length)];
            }
        }
    }
}
