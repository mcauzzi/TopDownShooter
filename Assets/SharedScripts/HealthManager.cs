using UnityEngine;

namespace SharedScripts
{
    public class HealthManager : MonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; private set; }
    
        public void DealDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
