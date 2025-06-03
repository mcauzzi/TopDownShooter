using TMPro;
using UnityEngine;
using Weapons;
using Weapons.Interfaces;

namespace UI.AmmoUI
{
    public class WeaponUiController : MonoBehaviour
    {
        public void Init(IWeaponWithAmmo weapon)
        {
            var weaponNameText = GetComponentInChildren<TextMeshProUGUI>();
            if (weaponNameText)
            {
                weaponNameText.text = weapon.GetWeaponName();
            }
            else
            {
                Debug.LogWarning("WeaponUiController: No TextMeshProUGUI component found in children.");
            }
            var ammoDisplay = GetComponentInChildren<AmmoDisplayController>();
            if (ammoDisplay)
            {
                ammoDisplay.Init(weapon);
            }
            else
            {
                Debug.LogWarning("WeaponUiController: No AmmoDisplayController component found in children.");
            }
        }

    }
}