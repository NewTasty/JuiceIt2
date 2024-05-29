
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

        private Transform _playerRef;
        private float _speed = 10;

        private void Awake()
        {
            float lRandomScale = Random.Range(0.2f, 0.7f);
            transform.localScale = new Vector3(lRandomScale, lRandomScale, lRandomScale);
        }

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
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundInstantiate(4);
            Destroy(gameObject);
        }
        
        private void OnMovement()
        {
            Vector3 lPosition = new Vector3(transform.position.x, 1f, transform.position.z); 
            Vector3 lTargetPosition = new Vector3(_playerRef.position.x, 1f, _playerRef.position.z);
                
            transform.position = Vector3.MoveTowards(lPosition, lTargetPosition, _speed/1000);
            
            transform.LookAt(_playerRef.position);
        }
    }
}
