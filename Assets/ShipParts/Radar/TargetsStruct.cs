using Shared.Scripts.IFF;
using UnityEngine;

namespace ShipParts.Radar
{
    public struct TargetsStruct
    {
        public readonly Transform Target;
        public readonly float     Distance;
        public readonly Vector2   Direction;
        public readonly Iff       Iff;

        public TargetsStruct(Transform target, float distance, Vector2 direction, Iff iff = Iff.None)
        {
            Target    = target;
            Distance  = distance;
            Direction = direction;
            Iff      = iff;
        }
    }
}