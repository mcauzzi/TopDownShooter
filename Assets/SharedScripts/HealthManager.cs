using SharedScripts.IFF;
using UnityEngine;

namespace SharedScripts
{
    public class HealthManager : MonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; private set; }
        
        public delegate void         OnDeath();
        public event OnDeath         onDeath;
        public delegate void         OnHealthChanged(int newHealth);
        public event OnHealthChanged onHealthChanged;
        [SerializeField] private Iff iff = Iff.None;
        public Iff Iff=> iff;
        public void TakeDamage(int damage)
        {
            Health -= damage;
            onHealthChanged?.Invoke(Health);
            if (Health <= 0)
            {
                onDeath?.Invoke();

                Destroy(gameObject);
            }
        }

     
    }
}