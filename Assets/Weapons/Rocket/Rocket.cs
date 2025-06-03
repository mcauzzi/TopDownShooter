using UnityEngine;
using Weapons.Interfaces;

namespace Weapons.Rocket
{
    public class Rocket : MonoBehaviour,IBullet
    {
        [SerializeField] private float speed    = 10f;
        [SerializeField] private float lifeTime = 5f;
        public                   float Range { get; private set; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Range=speed* lifeTime;
        }

        // Update is called once per frame
        void Update()
        {
            //move forwards
            transform.Translate(transform.up * (Time.deltaTime* speed), Space.World);
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
