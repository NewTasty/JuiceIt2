using JuiceIt2Content.Programming.Enemy;
using UnityEngine;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float lifeSpan = 4;
        [SerializeField] private GameObject HitEffect;
        [SerializeField] private Transform firstEffectAnchor;
        [SerializeField] private Transform secondEffectAnchor;
        [SerializeField] private GameObject trailEffect;
        [SerializeField] private GameObject moveEffect;

        private PlayerEngine _player;
        private Rigidbody _rb;

        private GameObject _moveEffectRef;
        private GameObject _trailEffectRef;
        
        private void Awake()
        {
            Destroy(gameObject, lifeSpan);
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _player = FindFirstObjectByType<PlayerEngine>();
            
            _moveEffectRef = Instantiate(moveEffect, firstEffectAnchor.position, transform.rotation, firstEffectAnchor);
            _trailEffectRef = Instantiate(trailEffect, secondEffectAnchor.position, transform.rotation, secondEffectAnchor);
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                if (HitEffect)
                {
                    Instantiate(HitEffect);
                }
                Destroy(gameObject);
            }else if (other.GetComponent<EnemyBasic>())
            {
                OnEnemyHitEffect(other);
            }
        }

        void OnEnemyHitEffect(Collider other)
        {
            if (HitEffect)
            {
                Instantiate(HitEffect);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
