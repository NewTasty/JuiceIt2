using System;
using System.Globalization;
using JuiceIt2Content.Programming.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JuiceIt2Content.Programming.UI
{
    public class LooseScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI points;
        
        private PlayerEngine _playerRef;

        private void Start()
        {
            Time.timeScale = 0;
            points.SetText(_playerRef.GetScore().ToString(CultureInfo.InvariantCulture));
        }

        public void Retry()
        {
            SceneManager.LoadScene("LVL_Dev");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("LVL_MainMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
