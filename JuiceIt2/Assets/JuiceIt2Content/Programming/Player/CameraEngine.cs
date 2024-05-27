using UnityEngine;

namespace JuiceIt2Content.Programming.Player
{
    public class CameraEngine : MonoBehaviour
    {
        [SerializeField] private float timeLerpSpeed;
        [SerializeField] private Vector3 offset = new Vector3(-10, -10, 10); 
        
        private Transform _playerRef;
        private Vector3 _velocity;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _playerRef = FindFirstObjectByType<PlayerEngine>().transform;
        }

        private void FixedUpdate()
        {
            CameraMovement();
        }

        private void CameraMovement()
        {
            Vector3 lNewPosition = new Vector3(_playerRef.position.x + offset.x, offset.y, _playerRef.position.z  + offset.z);
            transform.position = Vector3.SmoothDamp(transform.position, lNewPosition, ref _velocity, timeLerpSpeed);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            transform.position = offset;
        }
#endif
    }
}
