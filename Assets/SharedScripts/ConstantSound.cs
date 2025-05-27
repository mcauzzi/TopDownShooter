using UnityEngine;

namespace SharedScripts
{
    public class ConstantSound : MonoBehaviour
    {
        [SerializeField] private AudioClip   clip;
        private                  AudioSource _audioSource;
        [SerializeField,Range(0,1)] private float volume = 0.5f; // Default volume, can be adjusted in the inspector

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            
            _audioSource.loop        = true;
            _audioSource.playOnAwake = false;
            _audioSource.volume      = volume; // Set volume to a default value, can be adjusted later
            _audioSource.clip        = clip;
            _audioSource.Play();
        }

        // Update is called once per frame
        // void Update()
        // {
        //     if (clip)
        //     {
        //         if(clip.)
        //     }
        // }
    }
}
