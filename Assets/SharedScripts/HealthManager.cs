using Scenes.ManagementScripts;
using SharedScripts.IFF;
using UnityEngine;

namespace SharedScripts
{
    public class HealthManager : MonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; private set; }
        [SerializeField] private Iff  iff             = Iff.None;
        [SerializeField] private bool gameOverOnDeath = false;
        #region Events

        public delegate void                 OnDeathDelegate();
        public event OnDeathDelegate         OnDeath;
        public delegate void                 OnHealthChangedDelegate(int newHealth);
        public event OnHealthChangedDelegate OnHealthChanged;

        #endregion
        
        public Iff Iff=> iff;
        public void TakeDamage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke(Health);
            if (Health <= 0)
            {
                OnDeath?.Invoke();
                if (gameOverOnDeath)
                {
                    LevelManager.Instance?.LoadLevel(Levels.GameOver);
                }
                Destroy(gameObject);
            }
        }

     
    }
}