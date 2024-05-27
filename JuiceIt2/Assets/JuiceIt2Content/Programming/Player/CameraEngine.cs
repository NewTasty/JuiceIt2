using UnityEngine;

namespace JuiceIt2Content.Programming.Player
{
    public class CameraEngine : MonoBehaviour
    {
        [SerializeField] private float lerpSpeed;
        [SerializeField] private float offset = 15;
        
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
            Vector3 lNewPosition = new Vector3(_playerRef.position.x, offset, _playerRef.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, lNewPosition, ref _velocity, lerpSpeed);
        }
    }
}
