using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        void          FireStart();
        void          FireStop();
        public string GetWeaponName();
    }

    public delegate void OnAmmoChangedDelegate(int currentAmmo);
}