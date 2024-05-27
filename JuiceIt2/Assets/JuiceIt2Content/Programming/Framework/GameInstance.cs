using UnityEngine;

namespace JuiceIt2Content.Programming.Framework
{
    public class GameInstance : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
