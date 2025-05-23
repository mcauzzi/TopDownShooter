using SharedScripts;
using UnityEngine;

namespace Weapons
{
    public class OnHitExplosion:MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticles;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{gameObject.name} contact with {other.name}",gameObject);
            if (explosionParticles && GetComponent<DamageDealer>()?.CanDealDamage==true)
            {
                var particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                particles.Play();
                Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
            }
        }
    }
}