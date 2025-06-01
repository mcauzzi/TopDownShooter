using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Scripts
{
    [CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
    public class WaveConfigSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private float            moveSpeed = 5f;

        [SerializeField] public  float enemyInterval         = 1f;
        [SerializeField] private float enemyIntervalVariance = 0.2f;
        [SerializeField] private float minimumSpawnTime      = 0.1f;

        [SerializeField]
        private SerializableKeyValue<GameObject, int>[] weaponWeights =
            Array.Empty<SerializableKeyValue<GameObject, int>>();

        
        public  SerializableKeyValue<GameObject, int>[] WeaponWeights => weaponWeights;
        private int                                     _totalWeight;

        private void OnEnable()
        {
            weaponWeights = weaponWeights.GroupBy(x => x.Key)
                                         .Select(x =>
                                                     new SerializableKeyValue<GameObject, int>(x.Key,
                                                      x.Sum(y => y.Value)))
                                         .ToArray();
            _totalWeight = weaponWeights.Sum(x => x.Value);
        }

        public int WeaponsTotalWeight => _totalWeight;

        public int EnemyCount => enemyPrefabs.Count;

        public GameObject GetEnemyPrefab(int index)
        {
            return enemyPrefabs[index];
        }

        public float GetEnemyInterval()
        {
            var variance = enemyInterval + Random.Range(-enemyIntervalVariance, enemyIntervalVariance);
            return Mathf.Clamp(variance, minimumSpawnTime, enemyInterval + enemyIntervalVariance);
        }

        public float MoveSpeed => moveSpeed;
    }
}