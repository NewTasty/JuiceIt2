
using System;
using JuiceIt2Content.Programming.Framework;
using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JuiceIt2Content.Programming.Collectable
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private float xpToGive = 10;
        [SerializeField] private GameObject fxOnCollect;
        [SerializeField] private bool isAttractedEverywhere; 
        [SerializeField] private float attractedRadius = 10; 
        [SerializeField] private Vector2 scaleRangeValue = new Vector2(0.2f, 0.5f); 

        private Transform _playerRef;
        private float _speed = 10;
        private Rigidbody _rb;

        private void Awake()
        {
            float lRandomScale = Random.Range(scaleRangeValue.x, scaleRangeValue.y);
            transform.localScale = new Vector3(lRandomScale, lRandomScale, lRandomScale);

            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>().transform;

            Vector2 lRandomUnitVector = Random.insideUnitCircle / 0.5f; 
            _rb.AddExplosionForce(800, transform.position - new Vector3(scaleRangeValue.x, -1, scaleRangeValue.y),10);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (isAttractedEverywhere)
            {
                OnMovement();
            }
            else
            {
                float lDstanceWithPlayer = Vector3.Distance(transform.position, _playerRef.position);
                if (lDstanceWithPlayer <= attractedRadius)
                {
                    _speed += Time.deltaTime * 128;
                    OnMovement();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerEngine>()) return;
            
            _playerRef.GetComponent<PlayerEngine>().UpdateScore(xpToGive);
            if (fxOnCollect)
            {
                Instantiate(fxOnCollect, transform.position, transform.rotation);
            }
            GameMode.SoundManager.SoundInstantiate(4, this.transform);
            Destroy(gameObject);
        }
        
        private void OnMovement()
        {
            Vector3 lPosition = new Vector3(transform.position.x, 1f, transform.position.z); 
            Vector3 lTargetPosition = new Vector3(_playerRef.position.x, 1f, _playerRef.position.z);
                
            transform.position = Vector3.MoveTowards(lPosition, lTargetPosition, _speed/1000);
            
            transform.LookAt(_playerRef.position);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attractedRadius);
        }
#endif
    }
}
