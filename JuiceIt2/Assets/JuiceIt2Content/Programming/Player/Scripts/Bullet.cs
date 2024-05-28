using JuiceIt2Content.Programming.Enemy;
using UnityEngine;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float lifeSpan = 4;
        [SerializeField] private GameObject wallHitEffect;
        [SerializeField] private GameObject enemyHitEffect;

        private PlayerEngine _player;
        private Rigidbody _rb;
        
        private void Awake()
        {
            Destroy(gameObject, lifeSpan);
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _player = FindFirstObjectByType<PlayerEngine>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                if (wallHitEffect)
                {
                    Instantiate(wallHitEffect);
                }
                Destroy(gameObject);
            }else if (other.GetComponent<EnemyBasic>())
            {
                OnEnemyHitEffect(other);
            }
        }

        void OnEnemyHitEffect(Collider other)
        {
            _player.UpdateScore(10);
            if (enemyHitEffect)
            {
                Instantiate(enemyHitEffect);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
