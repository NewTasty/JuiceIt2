using System.Globalization;
using JuiceIt2Content.Programming.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JuiceIt2Content.Programming.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scorePoint;
        [SerializeField] private TextMeshProUGUI timeCount;
        [SerializeField] private Slider lifeBar;

        private PlayerEngine _playerRef;

        private float _time;
        
        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>();
            
            if (!_playerRef) return;
            lifeBar.maxValue = _playerRef.GetMaxLife();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            timeCount.SetText(Mathf.Round(_time).ToString(CultureInfo.InvariantCulture) + " sec");
            
            if (!_playerRef) return;
            scorePoint.SetText(_playerRef.GetScore().ToString(CultureInfo.CurrentCulture));

            lifeBar.value = _playerRef.GetLife();
        }
    }
}