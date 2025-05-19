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
        [SerializeField] private bool               isLooping        = false;

        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            do
            {
                foreach (var waveConfig in waveConfigs)
                {
                    yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfig));
                    yield return new WaitForSeconds(timeBetweenWaves);
                }
            } while (isLooping);
        }

        private IEnumerator SpawnAllEnemiesInWave(WaveConfigSO waveConfig)
        {
            for (var i = 0; i < waveConfig.EnemyCount; i++)
            {
                var enemyPrefab = waveConfig.GetEnemyPrefab(i);
                var spawnPoint  = waveConfig.GetStartingPoint();
                var enemy       = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);
                var pathfinder  = enemy.GetComponent<Pathfinder>();
                pathfinder.WaveConfig = waveConfig;
                yield return new WaitForSeconds(waveConfig.GetEnemyInterval());
            }
        }
    }
}