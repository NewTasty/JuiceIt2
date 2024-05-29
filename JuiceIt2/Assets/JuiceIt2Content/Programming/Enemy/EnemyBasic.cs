using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

namespace JuiceIt2Content.Programming.Enemy
{
    public class EnemyBasic : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private Vector2 rewardNumber = new (1, 5);
        [SerializeField] private GameObject rewardObject;
        [SerializeField] private GameObject deathFX;

        private Transform _playerRef;

        public UnityEvent onDeath;
    
        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>().transform;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 lPosition = new Vector3(transform.position.x, 1f, transform.position.z); 
            Vector3 lTargetPosition = new Vector3(_playerRef.position.x, 1f, _playerRef.position.z); 
            
            transform.position = Vector3.MoveTowards(lPosition, lTargetPosition, speed/1000);

            transform.LookAt(_playerRef.position);
        }

        public void SpawnCollectable()
        {
            if (deathFX)
            {
                Instantiate(deathFX, transform.position, transform.rotation);
            }
            
            int lRandom = (int)Random.Range(rewardNumber.x, rewardNumber.y);
            for (int i = 0; i < lRandom; i++)
            {
                if (rewardObject)
                {
                    float lRadius = 0.5f;
                    Vector3 lCircleSpawn = new Vector3(transform.position.x + Random.insideUnitCircle.x * lRadius, 0.5f, 
                                                       transform.position.z + Random.insideUnitCircle.y * lRadius);
                    Instantiate(rewardObject, lCircleSpawn, transform.rotation);
                }
            }
        }
    }
}
