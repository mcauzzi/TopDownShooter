using UnityEngine;

namespace Scenes.ManagementScripts
{
    public class MainMenuEvents : MonoBehaviour
    {
        public void OnStartGameClicked()
        {
            LevelManager.Instance.LoadLevel(Levels.Level1);
        }

        public void OnExitClicked()
        {
            Application.Quit(0);
        }
    }
}
