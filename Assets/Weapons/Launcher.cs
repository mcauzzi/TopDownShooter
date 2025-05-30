using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Launcher : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject launchablePrefab;
        [SerializeField] private float      fireRate   = 1f;
        private                  bool       _canFire;
        private Coroutine firingRoutine;

        public void FireStart()
        {
            if(firingRoutine!=null)
            {
                return;
            }
            _canFire = true;
            firingRoutine=StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (_canFire)
            {
                var rocket = Instantiate(launchablePrefab, transform.position, transform.rotation);
                yield return new WaitForSeconds(1 / fireRate);
            }
            firingRoutine = null;
        }

        public void FireStop()
        {
            _canFire = false;
        }
        public string GetWeaponName()
        {
            return "Launcher";
        }
    }
}