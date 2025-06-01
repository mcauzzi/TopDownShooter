using System;
using System.Collections;
using UnityEngine;

namespace Shared.Scripts
{
    public class AutoRespawner : MonoBehaviour
    {
        [SerializeField] private GameObject objectToRespawn;
        [SerializeField] private float      respawnDelay = 2f;

        private bool isSpawning = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(GetComponentsInChildren<Transform>().Length <= 1 &&!isSpawning) // Only the parent transform is present
            {
                isSpawning = true;
                this.DelayMethod(()=>
                                 {
                                     Instantiate(objectToRespawn, transform);
                                     isSpawning = false;
                                 }, respawnDelay);
            }
        }
    }

    public static class CoroutineHelpers
    {
        public static void DelayMethod(this MonoBehaviour behaviour,Action action, float delay)
        {
            behaviour.StartCoroutine(DelayHelper());
            return;

            IEnumerator DelayHelper()
            {
                yield return new WaitForSeconds(delay);
                action();
            }
        }
    }
}