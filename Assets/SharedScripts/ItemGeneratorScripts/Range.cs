using System;
using UnityEngine;

namespace SharedScripts.ItemGeneratorScripts
{
    [Serializable]
    public struct Range<T> where T : struct
    {
        [SerializeField] public T minValue;
        [SerializeField] public T maxValue;

        public Range(T minValue, T maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}