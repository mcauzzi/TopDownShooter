using UnityEngine;

namespace SharedScripts
{
    public class OnDeathEffects:MonoBehaviour
    {
        
        [SerializeField] private ParticleSystem onDeathParticles;
        [SerializeField] private AudioClip      onDeathSound;
        [SerializeField] private double         scoreValue = 100;
        private                  ScoreKeeper    _scoreKeeper;
        private void Start()
        {
            _scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
            var healthManager = GetComponent<HealthManager>();
            if (healthManager)
            {
                healthManager.onDeath += OnDeathHandler;
            }
            else
            {
                Debug.LogWarning("No HealthManager found on " + gameObject.name);
            }
        }

        private void OnDeathHandler()
        {
            if (onDeathParticles)
            {
                PlayHitEffect();
            }

            if (onDeathSound)
            {
                AudioSource.PlayClipAtPoint(onDeathSound, transform.position);
            }

            if (_scoreKeeper)
            {
                _scoreKeeper.Score += scoreValue;
            }
        }
        
        private void PlayHitEffect()
        {
            var particles = Instantiate(onDeathParticles, transform.position, Quaternion.identity);
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }
}