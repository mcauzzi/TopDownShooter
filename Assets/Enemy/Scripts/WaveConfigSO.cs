using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy.Scripts
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
    public class WaveConfigSO : ScriptableObject
    {
        [SerializeField] private Transform pathPrefab;

        [SerializeField]
        private float moveSpeed = 5f;

        public Transform GetStartingPoint()
        {
            return pathPrefab.GetChild(0);
        }

        public List<Transform> GetWaypoints()
        {
            return pathPrefab.Cast<Transform>().ToList();
        }
        public float MoveSpeed => moveSpeed;
    }
}