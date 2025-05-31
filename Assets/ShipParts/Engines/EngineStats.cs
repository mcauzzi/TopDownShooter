using System;
using UnityEngine;

namespace ShipParts.Engines
{
    [Serializable]
    public struct EngineStats
    {
        [field:SerializeField]
        public float MaxSpeed { get; set; }
        [field:SerializeField]
        public float Acceleration{get;set;}
        [field:SerializeField]
        public float RotationSpeed { get; set; }
    }
}