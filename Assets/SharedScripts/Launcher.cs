using System.Collections;
using UnityEngine;

namespace SharedScripts
{
    public class Launcher : MonoBehaviour, IFireable
    {
        [SerializeField] private GameObject launchablePrefab;
        [SerializeField] private float      fireRate   = 1f;
        private                  bool       _canFire;


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
                var rocket = Instantiate(launchablePrefab, transform.position, Quaternion.identity);
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