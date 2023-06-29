using Unity.Mathematics;
using UnityEngine;

namespace Games.DefenderTraining.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        private int _curHp;
        [SerializeField] private GameObject _explosionPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _curHp = _maxHp;
        }

        public bool TakeDamage(int hit)
        {
            _curHp -= hit;
            
            if (_curHp < 0)
            {
                _curHp = 0;
                OnDead();
                return true;
            }

            return false;
        }

        private void OnDead()
        {
            var explosionEffect = Instantiate(_explosionPrefab, transform.position, quaternion.identity);
            Destroy(explosionEffect, 1f);
            Destroy(gameObject);
        }
    }
}
