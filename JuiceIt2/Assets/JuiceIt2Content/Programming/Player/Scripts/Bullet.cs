using UnityEngine;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = 5;

        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
        }
        
    }
}
