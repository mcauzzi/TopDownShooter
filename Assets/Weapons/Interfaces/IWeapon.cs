namespace Weapons.Interfaces
{
    public interface IWeapon
    {
        void          FireStart();
        void          FireStop();
        public string GetWeaponName();
        public float  Range    { get; }
        public bool   IsFiring { get; }
    }

    public delegate void OnAmmoChangedDelegate(int currentAmmo);
}