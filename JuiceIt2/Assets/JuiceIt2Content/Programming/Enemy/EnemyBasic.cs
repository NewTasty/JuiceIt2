using JuiceIt2Content.Programming.Player;
using UnityEngine;

namespace JuiceIt2Content.Programming.Enemy
{
    public class EnemyBasic : MonoBehaviour
    {
        [SerializeField] private float speed = 300;

        private Transform _playerRef;
    
        private void Awake()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>().transform;
        }

        private void Update()
        {
            Vector3 lPosition = new Vector3(transform.position.x, 1f, transform.position.z); 
            Vector3 lTargetPosition = new Vector3(_playerRef.position.x, 1f, _playerRef.position.z); 
            
            transform.position = Vector3.MoveTowards(lPosition, lTargetPosition, speed/1000); 
        }
    }
}
