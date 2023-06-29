using Unity.Mathematics;
using UnityEngine;

namespace Games.DefenderTraining.Scripts.Enemy
{
    public class EnemyGenerator : MonoBehaviour
    {
        private int _numGenerated;
        [SerializeField] private int _capacity;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnLocation;
        private float _lastTimeGenerate = 0.0f;
        [SerializeField] private float _spawnCooldown = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            GenerateEnemies();
        }

        private void GenerateEnemies()
        {
            if (_numGenerated >= _capacity) return;

            if (Time.time < _lastTimeGenerate + _spawnCooldown) return;
            _lastTimeGenerate = Time.time;
            Instantiate(_enemyPrefab, _spawnLocation.position, quaternion.identity);
            _numGenerated++;
        }
    }
}
