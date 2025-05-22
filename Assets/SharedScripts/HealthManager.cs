using UnityEngine;

namespace SharedScripts
{
    public class HealthManager : MonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; private set; }
        [SerializeField] private ParticleSystem onHitParticles;
    
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (onHitParticles)
            {
                PlayHitEffect();
            }
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void PlayHitEffect()
        {
            var particles = Instantiate(onHitParticles, transform.position, Quaternion.identity);
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration+particles.main.startLifetime.constantMax);
        }
    }
}
