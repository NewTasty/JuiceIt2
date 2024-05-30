using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace JuiceIt2Content.Programming.Enemy
{
    public class EnemyBasic : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private int damage = 1;
        [SerializeField] private Vector2 rewardNumber = new (1, 5);
        [SerializeField] private GameObject rewardObject;
        [SerializeField] private GameObject deathFX;

        private Transform _playerRef;

        private NavMeshAgent _nma;

        public UnityEvent onDeath;

        private void Awake()
        {
            _nma = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>().transform;
            _nma.speed = speed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _nma.destination = _playerRef.position;
        }

        public void EnemyDeath()
        {
            if (deathFX)
            {
                Quaternion lRotation = Quaternion.LookRotation(-transform.forward);
                Instantiate(deathFX, transform.position, lRotation);
            }
            
            int lRandom = (int)Random.Range(rewardNumber.x, rewardNumber.y);
            for (int i = 0; i < lRandom; i++)
            {
                if (rewardObject)
                {
                    const float lRadius = 0.5f;
                    Vector3 lCircleSpawn = new Vector3(transform.position.x + Random.insideUnitCircle.x * lRadius, 1f, 
                                                       transform.position.z + Random.insideUnitCircle.y * lRadius);
                    Instantiate(rewardObject, lCircleSpawn, transform.rotation);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<PlayerEngine>())
            {
                other.GetComponent<PlayerEngine>().TakeDamage(damage);
            }
        }
    }
}
