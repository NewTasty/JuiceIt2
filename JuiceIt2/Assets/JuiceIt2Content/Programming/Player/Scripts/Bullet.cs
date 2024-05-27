using System;
using JuiceIt2Content.Programming.Enemy;
using UnityEngine;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float lifeSpan = 4;

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
            if (!other.GetComponent<EnemyBasic>()) return;
            print("EnemyHit");
            _player.UpdateScore(10);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }
}
