using UnityEngine;
using Weapons.Interfaces;

namespace Weapons
{
    public class WeaponGroup : MonoBehaviour
    {
        private IWeapon[] _associatedWeapons;

        private void Start()
        {
            _associatedWeapons = GetComponentsInChildren<IWeapon>();
            if (_associatedWeapons.Length == 0)
            {
                Debug.LogWarning("No weapons found in the WeaponGroup.",gameObject);
            }
        }

        public void FireStart()
        {
            foreach (var weapon in _associatedWeapons)
            {
                weapon.FireStart();
            }
        }
        
        public void FireStop()
        {
            foreach (var weapon in _associatedWeapons)
            {
                weapon.FireStop();
            }
        }
    }
}
