using UnityEngine;

namespace JuiceIt2Content.Programming.Player
{
    public class PlayerEngine : MonoBehaviour
    {
        [SerializeField] private int moveSpeed = 500;
        [SerializeField] private float acceleration = 0.3f;
        [SerializeField] private float deceleration = 0.3f;
        [SerializeField] private float baseFireSpeed = 1;
        [SerializeField] private float baseFirePower = 1;

        private void Start()
        {
            
        }
        
        
    }
}
