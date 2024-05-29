using JuiceIt2Content.Programming.Enemy;
using UnityEngine;

namespace JuiceIt2Content.Programming.Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float lifeSpan = 4;
        [SerializeField] private GameObject hitEffect;

        private Rigidbody _rb;
        
        private void Awake()
        {
            Destroy(gameObject, lifeSpan);
            _rb = GetComponent<Rigidbody>();
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundInstantiate(0);
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                if (hitEffect)
                {
                    Instantiate(hitEffect, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }else if (other.GetComponent<EnemyBasic>())
            {
                OnEnemyHitEffect(other);
            }
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundInstantiate(1);
        }

        private void OnEnemyHitEffect(Collider other)
        {
            if (hitEffect)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
            other.GetComponent<EnemyBasic>().onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
