using SharedScripts.ItemGeneratorScripts;
using UnityEngine;

namespace Weapons.GuidedMissile
{
    [CreateAssetMenu(fileName = "GuidedMissileGenerator", menuName = "Scriptable Objects/GuidedMissileGenerator")]
    public class GuidedMissileAffixes : ScriptableObject
    {
        [SerializeField] private Range<int>[]        additionalFireRatePercentages;
        [SerializeField] private Range<Range<int>>[] additionalFirePower;
        
        public AdditionalFireRatePercentage GetNewFireRate()
        {
            return new AdditionalFireRatePercentage(additionalFireRatePercentages);
        }
        public FirePower GetNewFirePower()
        {
            return new FirePower(additionalFirePower);
        }
    }
}