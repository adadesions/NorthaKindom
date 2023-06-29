using Games.DefenderTraining.Scripts.Enemy;
using Unity.Mathematics;
using UnityEngine;

namespace Games.DefenderTraining.Scripts.Defender
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionEffectPrefab;
        [SerializeField] private int _damage = 1;
        private DefenderController _Shooter;

        public DefenderController Shooter
        {
            get => _Shooter;
            set => _Shooter = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            var explosion = Instantiate(_explosionEffectPrefab, transform.position, quaternion.identity);
            Destroy(explosion,1.0f);

            if (other.CompareTag("Attacker"))
            {
                var enemyHealth = other.GetComponent<EnemyHealth>();
                var isDead = enemyHealth.TakeDamage(_damage);
                if (isDead)
                {
                    _Shooter.UpdateKillCounter();
                }
            }
        }
    }
}
