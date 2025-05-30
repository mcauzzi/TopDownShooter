using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.ManagementScripts
{
    public class LevelManager : MonoBehaviour
    {
        public static  LevelManager Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLevel(Levels level)
        {
            SceneManager.LoadScene(level.ToString());
        }
    }
}
