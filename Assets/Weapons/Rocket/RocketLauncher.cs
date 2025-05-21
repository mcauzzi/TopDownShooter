using System;
using SharedScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons.Rocket
{
    public class RocketLauncher : MonoBehaviour,IFireable
    {
         [SerializeField]private GameObject rocketPrefab;
        private void Start()
        {
            
        }

        public void Fire()
        {
            Instantiate(rocketPrefab,transform.position, Quaternion.identity);
        }
    }
}
