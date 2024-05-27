using System.Globalization;
using JuiceIt2Content.Programming.Player.Scripts;
using TMPro;
using UnityEngine;

namespace JuiceIt2Content.Programming.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scorePoint;

        private PlayerEngine _playerRef;
        
        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>();
        }

        private void Update()
        {
            if (!_playerRef) return;
            scorePoint.SetText(_playerRef.GetScore().ToString(CultureInfo.CurrentCulture));
        }
    }
}