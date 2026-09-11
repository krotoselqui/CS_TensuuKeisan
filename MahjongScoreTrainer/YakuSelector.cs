using System;

namespace MahjongScoreTrainer
{
    internal sealed class YakuWeight
    {
        internal readonly int Id;
        internal readonly int Weight;

        internal YakuWeight(int id, int weight)
        {
            Id = id;
            Weight = weight;
        }
    }

    internal static class YakuSelector
    {
        internal static readonly YakuWeight[] Entries =
        {
            new YakuWeight(YakuNumbers.Pinfu, 800), new YakuWeight(YakuNumbers.Tanyao, 1000), new YakuWeight(YakuNumbers.Iipeikou, 500),
            new YakuWeight(YakuNumbers.Yakuhai, 500), new YakuWeight(YakuNumbers.YakuhaiRoundWind, 500), new YakuWeight(YakuNumbers.YakuhaiSeatWind, 500),
            new YakuWeight(YakuNumbers.YakuhaiDragon, 500), new YakuWeight(YakuNumbers.Chiitoitsu, 200), new YakuWeight(YakuNumbers.Honchantaiyaochuu, 200),
            new YakuWeight(YakuNumbers.Ittsu, 200), new YakuWeight(YakuNumbers.SanshokuDoujun, 200), new YakuWeight(YakuNumbers.SanshokuDoukou, 80),
            new YakuWeight(YakuNumbers.Sankantsu, 200), new YakuWeight(YakuNumbers.Toitoi, 200), new YakuWeight(YakuNumbers.Sanankou, 200),
            new YakuWeight(YakuNumbers.Shousangen, 200), new YakuWeight(YakuNumbers.Honroutou, 200), new YakuWeight(YakuNumbers.Ryanpeikou, 100),
            new YakuWeight(YakuNumbers.Junchantaiyaochuu, 100), new YakuWeight(YakuNumbers.Honitsu, 100), new YakuWeight(YakuNumbers.Chinitsu, 100),
            new YakuWeight(YakuNumbers.Daisangen, 20), new YakuWeight(YakuNumbers.Suuankou, 20), new YakuWeight(YakuNumbers.SuuankouTanki, 1),
            new YakuWeight(YakuNumbers.Tsuiisou, 2), new YakuWeight(YakuNumbers.Ryuuiisou, 1), new YakuWeight(YakuNumbers.Chinroutou, 1),
            new YakuWeight(YakuNumbers.ChuurenPoutou, 3), new YakuWeight(YakuNumbers.JunseiChuurenPoutou, 1), new YakuWeight(YakuNumbers.KokushiMusou, 3),
            new YakuWeight(YakuNumbers.JunseiKokushiMusou, 1), new YakuWeight(YakuNumbers.Daisuushi, 1), new YakuWeight(YakuNumbers.Shousuushi, 3),
            new YakuWeight(YakuNumbers.Suukantsu, 0)
        };

        internal static int Select(Random random)
        {
            int totalWeight = 0;
            foreach (YakuWeight entry in Entries) totalWeight += entry.Weight;
            int selected = random.Next(totalWeight);
            foreach (YakuWeight entry in Entries)
            {
                if (selected < entry.Weight) return entry.Id;
                selected -= entry.Weight;
            }
            throw new InvalidOperationException("No yaku selected.");
        }
    }
}
