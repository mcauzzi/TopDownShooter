using System;
using UnityEngine;

namespace SharedScripts
{
    public class DamageDealer:MonoBehaviour
    {
        [SerializeField] private int   damage = 10;
        public                   int   Damage => damage;
        [SerializeField] private bool  destroyOnHit = true;
        [SerializeField] private float timeBeforeEnabled;
        private bool _enabled;
        private void Start()
        {
            _enabled = false;
            if (timeBeforeEnabled > 0)
            {
                Invoke(nameof(Enable), timeBeforeEnabled);
            }
            else
            {
                Enable();
            }
        }

        private void Enable()
        {
            _enabled = true;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthManager>();
            if (health && _enabled)
            {
                health.DealDamage(damage);
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}