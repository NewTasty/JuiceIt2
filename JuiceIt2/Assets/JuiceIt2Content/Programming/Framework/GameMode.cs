using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;

namespace JuiceIt2Content.Programming.Framework
{
    public class GameMode : MonoBehaviour
    {
        [SerializeField] private GameObject looseScreenRef;
        
        public static SoundManager SoundManager;
        private PlayerEngine _playerRef;

        private bool _victoryCondition;
        private bool _looseScreen;
        
        private void Awake()
        {
            Time.timeScale = 1;
            
            SoundManager = FindFirstObjectByType<SoundManager>();
            _playerRef = FindFirstObjectByType<PlayerEngine>();
        }

        private void Start()
        {
            _victoryCondition = false;
        }

        private void Update()
        {
            if (_playerRef.GetLife() <= 0)
            {
                _victoryCondition = true;
            }
            
            if (_victoryCondition && !_looseScreen)
            {
                Instantiate(looseScreenRef);
                _looseScreen = true;
            }
        }
    }
}
