using Player.Scripts;
using UnityEngine;
using Weapons;

namespace UI.AmmoUI
{
    public class WeaponUiGroup : MonoBehaviour
    {
        [SerializeField] private GameObject ammoGroupPrefab;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var playerWeapons = FindAnyObjectByType<PlayerWeaponController>().GetComponentsInChildren<IWeaponWithAmmo>();
            foreach (var playerWeapon in playerWeapons)
            {
                var ammoGroup          = Instantiate(ammoGroupPrefab, transform);
                var weaponUiController = ammoGroup.GetComponent<WeaponUiController>();
                weaponUiController.Init(playerWeapon);
            }
        }
    }
}
