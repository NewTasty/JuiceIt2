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

        private void Awake()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>();
            points.SetText(_playerRef.GetScore().ToString(CultureInfo.InvariantCulture));
        }

        private void Start()
        {
            Time.timeScale = 0;
        }

        public void Retry()
        {
            Time.timeScale = 0;
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
