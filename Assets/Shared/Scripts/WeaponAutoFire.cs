using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Weapons.Interfaces;

namespace Shared.Scripts
{
    public class WeaponAutoFire : MonoBehaviour
    {
        private List<IWeapon> _weapons = new();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var weapon in _weapons)
            {
                weapon.FireStart();
            }
        }

        public void AddWeapon(GameObject weapon)
        {
            if (!weapon)
            {
                return;
            }
            var clone    =Instantiate(weapon,gameObject.transform);
            Debug.Log("Adding weapon: " + clone.name);
           
            var fireable = clone.GetComponent<IWeapon>();
            if (fireable == null)
            {
                Debug.LogError($"GameObject {clone.name} does not implement IFireable interface.");
                return;
            }
            var weaponSlot = gameObject.FindComponentInChildWithTag<Transform>("WeaponSlot");
            clone.transform.position = weaponSlot.transform.position;
           
            _weapons.Add(fireable);
        }
    }
}
