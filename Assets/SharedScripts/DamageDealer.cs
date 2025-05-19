using System;
using UnityEngine;

namespace SharedScripts
{
    public class DamageDealer:MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        public                   int Damage => damage;
        [SerializeField] private bool destroyOnHit = true;

        public void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthManager>();
            if (health)
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