using System;
using UnityEngine;

namespace Weapons.Rocket
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //move forwards
            transform.Translate(Vector2.up * (Time.deltaTime* speed));
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
