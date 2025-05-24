using System.Collections;
using System.Collections.Generic;
using SharedScripts;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

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
                var enemy       = Instantiate(enemyPrefab, spawnPoint.position,enemyPrefab.transform.rotation, transform);
                AssignPath(waveConfig, enemy);
                AssignWeapon(waveConfig,enemy);
                yield return new WaitForSeconds(waveConfig.GetEnemyInterval());
            }
        }

        private void AssignWeapon(WaveConfigSO config,GameObject enemy)
        {
            var weaponWeights = config.WeaponWeights;
            if (weaponWeights.Length == 0) return;

            var randomValue = Random.Range(0, config.WeaponsTotalWeight);
            GameObject selectedWeapon =null;

            foreach (var weight in weaponWeights)
            {
                if (randomValue < weight.Value)
                {
                    selectedWeapon = weight.Key;
                    break;
                }
                randomValue -= weight.Value;
            }
            var autoFireComponent = enemy.GetComponent<WeaponAutoFire>();
            if (autoFireComponent)
            {
                autoFireComponent.AddWeapon(selectedWeapon);
            }
            else
            {
                Debug.LogWarning($"No WeaponAutoFire component found on {enemy.name}");
            }
        
        }

        private static void AssignPath(WaveConfigSO waveConfig, GameObject enemy)
        {
            var pathfinder  = enemy.GetComponent<Pathfinder>();
            pathfinder.WaveConfig = waveConfig;
        }
    }
}