using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player.Scripts
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private                                 WeaponGroup[] _weaponGroups;
        private                                 int   _selectedWeaponGroup;

        private void Start()
        {
            _weaponGroups      = GetComponentsInChildren<WeaponGroup>();
            Debug.Log($"Loaded {_weaponGroups.Length} weapons");
            _selectedWeaponGroup = 0;
        }
        public void OnAttack(InputValue value)
        {
            if (value.isPressed)
            {
                _weaponGroups[_selectedWeaponGroup].FireStart();
            }
            else
            {
                _weaponGroups[_selectedWeaponGroup].FireStop();
            }
        }

        public void OnNext(InputValue value)
        {
            if(_selectedWeaponGroup+1< _weaponGroups.Length)
            {
                _selectedWeaponGroup++;
            }
            else
            {
                _selectedWeaponGroup = 0;
            }
            Debug.Log("Selected Weapon: " + _selectedWeaponGroup);
        }
        public void OnPrevious(InputValue value)
        {
            if(_selectedWeaponGroup-1 >= 0)
            {
                _selectedWeaponGroup--;
            }
            else
            {
                _selectedWeaponGroup = _weaponGroups.Length - 1;
            }
            Debug.Log("Selected Weapon: " + _selectedWeaponGroup);
        }
    }
}