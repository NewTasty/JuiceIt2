using UnityEngine;

namespace JuiceIt2Content.Programming.Player.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = 5;
        [SerializeField] private float lifeSpan = 4;

        private void Awake()
        {
            Destroy(gameObject, lifeSpan);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.GetMask("Enemies")) return;
            print("Ennemy");
            other.gameObject.GetComponent<PlayerEngine>().UpdateScore(10);
            Destroy(other.gameObject);
        }
        
    }
}
