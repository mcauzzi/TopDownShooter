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

    [Flags]
    public enum EngineStatus
    {
        Idle=0,
        Accelerating=1,
        Decelerating=1<<1,
        RotatingLeft=1<<2,
        RotatingRight=1<<3,
    }
}