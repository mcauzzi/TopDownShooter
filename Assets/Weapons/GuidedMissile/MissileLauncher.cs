using Shared.Scripts;
using Shared.Scripts.IFF;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Weapons.Interfaces;

namespace Weapons.GuidedMissile
{
    public class MissileLauncher : MonoBehaviour, IWeaponWithAmmo
    {
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private int        maxMissileStored = 5;
        [SerializeField] private float      reloadTime       = 2f;
        private                  int        _currentMissileStored;
        private                  float      _reloadTimer;
        public                   float      Range { get; private set; }
        private                  Iff        _iff;
        public bool IsFiring { get; private set; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentMissileStored                             = maxMissileStored;
            _iff                                              = GetComponentInParent<HealthManager>()?.Iff ?? Iff.None;
        }

        // Update is called once per frame
        void Update()
        {
            _reloadTimer += Time.deltaTime;
            if (_reloadTimer >= reloadTime && _currentMissileStored < maxMissileStored)
            {
                _currentMissileStored++;
                OnAmmoChanged?.Invoke(_currentMissileStored);
                _reloadTimer = 0;
            }
        }

        public void FireStart()
        {
            IsFiring = true;
            if (_currentMissileStored > 0)
            {
                var missile = Instantiate(missilePrefab, transform.position, transform.rotation);
                missile.GetComponent<MissileGuidance>().Iff= _iff;
                _currentMissileStored--;
                OnAmmoChanged?.Invoke(_currentMissileStored);
            }

            IsFiring = false;
        }

        public void FireStop()
        {
            IsFiring = false;
        }

        public string GetWeaponName()
        {
            return "Guided Missile Launcher";
        }

        public event OnAmmoChangedDelegate OnAmmoChanged;

        public int GetMaxAmmo()
        {
            return maxMissileStored;
        }

        [field:SerializeField]public Image AmmoIconPrefab { get; set; }
    }
}