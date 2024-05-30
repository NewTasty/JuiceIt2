using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;

namespace JuiceIt2Content.Programming.Framework
{
    public class GameMode : MonoBehaviour
    {
        public static SoundManager SoundManager;

        private PlayerEngine _playerRef;
        
        private void Awake()
        {
            SoundManager = FindFirstObjectByType<SoundManager>();
            print(SoundManager);
        }

        private void Update()
        {
            if (_playerRef.GetLife() == 0)
            {
                //SceneManager.LoadScene("LVL_Loose");
            }
        }
    }
}
