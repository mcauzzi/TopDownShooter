using System;
using UnityEngine;

namespace Player.Scripts
{
    [Serializable]
    public struct Padding
    {
        [SerializeField,Range(0, 1)] public float Left;
        [SerializeField,Range(0, 1)] public float Right;
        [SerializeField,Range(0, 1)] public float Top;
        [SerializeField,Range(0, 1)] public float Bottom;
        public override string ToString()
        {
            return $"Padding: Left={Left}, Right={Right}, Top={Top}, Bottom={Bottom}";
        }
    }
}