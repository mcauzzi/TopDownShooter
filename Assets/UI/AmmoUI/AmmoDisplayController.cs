using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI.AmmoUI
{
    public class AmmoDisplayController : MonoBehaviour
    {
        private Image[] _ammoIcons;
        public void Init(IWeaponWithAmmo weapon)
        {
            weapon.OnAmmoChanged += UpdateAmmoUI;
            var maxAmmo = weapon.GetMaxAmmo();
            _ammoIcons= new Image[maxAmmo];
            for (var i = 0; i < maxAmmo; i++)
            {
                var ammoIcon = Instantiate(weapon.AmmoIconPrefab, transform);
                ammoIcon.name = $"AmmoIcon_{i}";
                _ammoIcons[i] = ammoIcon;
            }
        }

        private void UpdateAmmoUI(int currentAmmo)
        {
            for (var i = 0; i < _ammoIcons.Length; i++)
            {
                var isActive = i < currentAmmo;
                _ammoIcons[i].gameObject.SetActive(isActive);
            }
        }
    }
}