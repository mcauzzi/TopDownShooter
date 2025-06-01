using UnityEngine;

namespace Shared.Scripts
{
    public class DamageDealer:MonoBehaviour
    {
        [SerializeField] private int   damage = 10;
        public           int   Damage => damage;
        [SerializeField] private bool  destroyOnHit = true;
        [SerializeField] private float timeBeforeCanDealDamage;

        private void Start()
        {
            CanDealDamage = false;
            if (timeBeforeCanDealDamage > 0)
            {
                Invoke(nameof(Enable), timeBeforeCanDealDamage);
            }
            else
            {
                Enable();
            }
        }
        public bool CanDealDamage { get; private set; }

        private void Enable()
        {
            CanDealDamage = true;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthManager>();
            if (health && CanDealDamage)
            {
                health.TakeDamage(damage);
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}