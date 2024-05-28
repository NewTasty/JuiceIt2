using UnityEngine;

namespace JuiceIt2Content.Programming.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float spawnRate;
        [SerializeField] private float spawnRateModifierProbability;
        [SerializeField] private GameObject enemy;

        private float _timer;
        private BoxCollider _box;
        
        private void Start()
        {
            
        }

        private void Update()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (_timer <= spawnRate)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Instantiate(enemy);
                _timer = 0;
            }
        }
    }
}
