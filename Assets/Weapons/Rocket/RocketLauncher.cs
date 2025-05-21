using System;
using System.Collections;
using SharedScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons.Rocket
{
    public class RocketLauncher : MonoBehaviour, IFireable
    {
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private float      fireRate   = 1f;
        private                  bool       _canFire;

        private void Start()
        {
        }

        private void Update()
        {
           
        }

        public void FireStart()
        {
            Debug.Log("Firing Start");
            _canFire = true;
            StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (_canFire)
            {
                var rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1 / fireRate);
            }
        }

        public void FireStop()
        {
            Debug.Log("Firing Stop");
            _canFire = false;
        }
    }
}