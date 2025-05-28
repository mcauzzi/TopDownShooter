using SharedScripts;
using SharedScripts.IFF;
using UnityEngine;

namespace Weapons.GuidedMissile
{
    public class MissileLauncher : MonoBehaviour,IWeapon
    {
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private int        maxMissileStored = 5;
        [SerializeField] private float      reloadTime       = 2f;
        private                  int        _currentMissileStored;
        private                  float      _reloadTimer;

        private Iff _iff;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentMissileStored = maxMissileStored;
            _iff                  = GetComponentInParent<HealthManager>()?.Iff?? Iff.None;
            //Create a pool for missiles
        }

        // Update is called once per frame
        void Update()
        {
            _reloadTimer+= Time.deltaTime;
            if (_reloadTimer >= reloadTime && _currentMissileStored < maxMissileStored)
            {
                _currentMissileStored++;
                _reloadTimer = 0;
            }
        }

        public void FireStart()
        {
            if (_currentMissileStored > 0)
            {
                Debug.Log("Fired");
                var missile = Instantiate(missilePrefab, transform.position,transform.rotation);
                missile.GetComponent<MissileGuidance>().Iff = _iff;
                _currentMissileStored--;
            }
        }

        public void FireStop()
        {
        }
    }
}
