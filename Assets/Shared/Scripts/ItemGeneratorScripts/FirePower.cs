using System;

namespace Shared.Scripts.ItemGeneratorScripts
{
    public struct FirePower : IAffix<Range<int>>
    {
        private static Random     Rnd = new(Guid.NewGuid().GetHashCode());
        public         Range<int> Value { get; }

        public string Description => $"Granting between {Value.minValue} and {Value.maxValue} additional firepower";

        private FirePower(Range<int> value)
        {
            Value = value;
        }

        public FirePower(Range<Range<int>>[] tiers)
        {
            var tier     = tiers[Rnd.Next(0, tiers.Length)];
            var minValue = Rnd.Next(tier.minValue.minValue, tier.minValue.maxValue + 1);
            var maxValue = Rnd.Next(tier.maxValue.minValue, tier.maxValue.maxValue + 1);
            Value=new Range<int>(minValue, maxValue);
        }
    }
}