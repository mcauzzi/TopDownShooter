using UnityEngine.UI;

namespace Weapons
{
    public interface IWeaponWithAmmo : IWeapon
    {
        public event OnAmmoChangedDelegate OnAmmoChanged;
        int                         GetMaxAmmo();
        public Image                AmmoIconPrefab { get;  }
    }
}