using System.Collections;
using UnityEngine;
using Weapons.Interfaces;

namespace Weapons
{
    public class Launcher : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject launchablePrefab;
        [SerializeField] private float      fireRate   = 1f;
        private                  Coroutine  firingRoutine;
        public                   float      Range { get; private set; }
        public bool      IsFiring { get; private set; }
        public void Start()
        {
            var bullet= launchablePrefab.GetComponent<IBullet>();
            Range = bullet.Range;
        }
        public void FireStart()
        {
            if(firingRoutine!=null)
            {
                return;
            }
            IsFiring = true;
            firingRoutine=StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (IsFiring)
            {
                var rocket = Instantiate(launchablePrefab, transform.position, transform.rotation);
                yield return new WaitForSeconds(1 / fireRate);
            }
            firingRoutine = null;
        }

        public void FireStop()
        {
            IsFiring = false;
        }
        public string GetWeaponName()
        {
            return "Launcher";
        }
    }
}