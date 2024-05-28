using UnityEngine;
using UnityEngine.SceneManagement;

namespace JuiceIt2Content.Programming.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel()
        {
            SceneManager.LoadScene("LVL_Dev");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
