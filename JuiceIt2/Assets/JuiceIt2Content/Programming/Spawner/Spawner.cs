using UnityEngine;
namespace JuiceIt2Content.Programming.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float spawnBySec;
        [SerializeField, Range(0, 1)] private float spawnRateModifierProbability;
        [SerializeField] private GameObject enemy;

        private float _timer;
        private BoxCollider _box;
        private float _Proba;
        
        private void Awake()
        {
            _box = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (_timer <= spawnBySec)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                Vector3 lRandomPosition = new Vector3(
                    Random.Range(_box.bounds.min.x, _box.bounds.max.x), 0,
                    Random.Range(_box.bounds.min.z, _box.bounds.max.z));
                
                Instantiate(enemy, lRandomPosition, transform.rotation);
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundInstantiate(2);

                _timer = 0;
            }
        }
    }
}
