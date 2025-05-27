using UnityEngine;

namespace SharedScripts
{
    public class HealthManager : MonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; private set; }
        [SerializeField] private ParticleSystem onDeathParticles;
        [SerializeField] private AudioClip onDeathSound;
    
        public void TakeDamage(int damage)
        {
            Health -= damage;
           
            if (Health <= 0)
            {
                if (onDeathParticles)
                {
                    PlayHitEffect();
                }
                if (onDeathSound)
                {
                    AudioSource.PlayClipAtPoint(onDeathSound, transform.position);
                }
                Destroy(gameObject);
            }
        }

        private void PlayHitEffect()
        {
            var particles = Instantiate(onDeathParticles, transform.position, Quaternion.identity);
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration+particles.main.startLifetime.constantMax);
        }
    }
}
