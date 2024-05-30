using UnityEngine;

namespace JuiceIt2Content.Programming.Framework
{
    public class GameMode : MonoBehaviour
    {
        public static SoundManager SoundManager;

        private void Awake()
        {
            SoundManager = FindFirstObjectByType<SoundManager>();
        }
    }
}
