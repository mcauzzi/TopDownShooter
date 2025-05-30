using System;
using Random = System.Random;

namespace SharedScripts.ItemGeneratorScripts
{
    public struct AdditionalFireRatePercentage : IAffix<int>
    {
        private static Random Rnd = new Random(Guid.NewGuid().GetHashCode());
        public         int    Value { get; private set; }

        public string Description => $"{Value}% increased firerate";

        private AdditionalFireRatePercentage(int value)
        {
            Value = value;
        }
        public AdditionalFireRatePercentage(Range<int>[] tiers)
        {
            var tier = tiers[Rnd.Next(0, tiers.Length)];
            Value=(Rnd.Next(tier.minValue, tier.maxValue + 1));
        }
    }
}