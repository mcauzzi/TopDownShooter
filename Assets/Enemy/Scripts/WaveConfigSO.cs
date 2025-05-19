using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.Scripts
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
    public class WaveConfigSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private Transform        pathPrefab;
        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField] public  float enemyInterval         = 1f;
        [SerializeField] private float enemyIntervalVariance = 0.2f;
        [SerializeField] private float minimumSpawnTime      = 0.1f;
        public Transform GetStartingPoint()
        {
            return pathPrefab.GetChild(0);
        }
        public int EnemyCount=> enemyPrefabs.Count;
        public GameObject GetEnemyPrefab(int index)
        {
            return enemyPrefabs[index];
        }

        public List<Transform> GetWaypoints()
        {
            return pathPrefab.Cast<Transform>().ToList();
        }
        public float GetEnemyInterval()
        {
            var variance= enemyInterval + Random.Range(-enemyIntervalVariance, enemyIntervalVariance);
            return Mathf.Clamp(variance, minimumSpawnTime,enemyInterval+enemyIntervalVariance);
        }
        public float MoveSpeed => moveSpeed;
    }
}