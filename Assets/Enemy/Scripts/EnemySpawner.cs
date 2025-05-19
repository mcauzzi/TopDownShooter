using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfigSO> waveConfigs;
        [SerializeField] private float              timeBetweenWaves = 0.5f;
        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            foreach (var waveConfig in waveConfigs)
            {
                yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfig));
                yield return new WaitForSeconds(timeBetweenWaves);
            }
         
        }

        private IEnumerator SpawnAllEnemiesInWave(WaveConfigSO waveConfig)
        {
            for (var i = 0; i < waveConfig.EnemyCount; i++)
            {
                var enemyPrefab = waveConfig.GetEnemyPrefab(i);
                var spawnPoint  = waveConfig.GetStartingPoint();
                var enemy       = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity,transform);
                var pathfinder  = enemy.GetComponent<Pathfinder>();
                pathfinder.WaveConfig = waveConfig;
                yield return new WaitForSeconds(waveConfig.GetEnemyInterval());
            }
        }
    }
}
