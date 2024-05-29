using System.Globalization;
using JuiceIt2Content.Programming.Player.Scripts;
using TMPro;
using UnityEngine;

namespace JuiceIt2Content.Programming.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scorePoint;
        [SerializeField] private TextMeshProUGUI timeCount;

        private PlayerEngine _playerRef;

        private float _time;
        
        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            timeCount.SetText(Mathf.Round(_time).ToString(CultureInfo.InvariantCulture));
            
            if (!_playerRef) return;
            scorePoint.SetText(_playerRef.GetScore().ToString(CultureInfo.CurrentCulture));
        }
    }
}