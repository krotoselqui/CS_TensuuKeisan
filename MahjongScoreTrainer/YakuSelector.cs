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
            new YakuWeight(7, 800), new YakuWeight(8, 1000), new YakuWeight(9, 500),
            new YakuWeight(14, 500), new YakuWeight(18, 500), new YakuWeight(19, 500),
            new YakuWeight(20, 500), new YakuWeight(22, 200), new YakuWeight(23, 200),
            new YakuWeight(24, 200), new YakuWeight(25, 200), new YakuWeight(26, 80),
            new YakuWeight(27, 200), new YakuWeight(28, 200), new YakuWeight(29, 200),
            new YakuWeight(30, 200), new YakuWeight(31, 200), new YakuWeight(32, 100),
            new YakuWeight(33, 100), new YakuWeight(34, 100), new YakuWeight(35, 100),
            new YakuWeight(39, 20), new YakuWeight(40, 20), new YakuWeight(41, 1),
            new YakuWeight(42, 2), new YakuWeight(43, 1), new YakuWeight(44, 1),
            new YakuWeight(45, 3), new YakuWeight(46, 1), new YakuWeight(47, 3),
            new YakuWeight(48, 1), new YakuWeight(49, 1), new YakuWeight(50, 3),
            new YakuWeight(51, 0)
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
