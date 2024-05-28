using JuiceIt2Content.Programming.Player.Scripts;
using UnityEngine;

namespace JuiceIt2Content.Programming.Collectable
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private float xpToGive = 10;
        [SerializeField] private float speed = 10;
        [SerializeField] private GameObject fxOnCollect;
        [SerializeField] private bool isAttractedEverywhere; 
        [SerializeField] private float attractedRadius = 10; 

        private Transform _playerRef;
    
        private void Awake()
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
                lOnMovement();
            }
            else
            {
                float lDstanceWithPlayer = Vector3.Distance(transform.position, _playerRef.position);
                 print(lDstanceWithPlayer);
                if (lDstanceWithPlayer <= attractedRadius)
                {
                    speed += Time.deltaTime * 16;
                    lOnMovement();
                }
            }
            
            void lOnMovement()
            {
                Vector3 lPosition = new Vector3(transform.position.x, 1f, transform.position.z); 
                Vector3 lTargetPosition = new Vector3(_playerRef.position.x, 1f, _playerRef.position.z);
                
                transform.position = Vector3.MoveTowards(lPosition, lTargetPosition, speed/1000);
            
                transform.LookAt(_playerRef.position);
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
            Destroy(gameObject);
        }
    }
}
