using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Games.DefenderTraining.Scripts.Defender
{
    public class DefenderController : MonoBehaviour
    {
        private Transform _lockTarget;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _weaponInitialSpeed = 30.0f;
        private float _lastAttackTime = 0.0f;
        [SerializeField] private float _attackCoolDown = 0.5f;
        private Vector3 _direction;
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _turnSpeed = 2.0f;
        private TextMeshPro _killCountText;
        private int _killCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            _lockTarget = null;
            _killCountText = GetComponentInChildren<TextMeshPro>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            AttackEnemy();
        }

        private void AttackEnemy()
        {
            if (!_lockTarget) return;
            
            TurnToLockTarget();

            if (Time.time < _lastAttackTime + _attackCoolDown) return;

            _lastAttackTime = Time.time;
            var bullet = Instantiate(_bulletPrefab, _muzzle.position, quaternion.identity);
            if (bullet.TryGetComponent<BulletController>(out var controller))
            {
                controller.Shooter = this;
            }
            
            if (bullet.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = Time.fixedDeltaTime * _weaponInitialSpeed * _direction;
                var effect = Instantiate(_effectPrefab, rb.position, quaternion.identity);
                Destroy(effect, 0.2f);
            }
            Destroy(bullet, 1.0f);
        }

        private void TurnToLockTarget()
        {
            _direction = _lockTarget.position - transform.position;
            var newRotation = Quaternion.LookRotation(-_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime * _turnSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Attacker") && !_lockTarget)
            {
                _lockTarget = other.transform;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Attacker") && !_lockTarget)
            {
                _lockTarget = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_lockTarget) return;
            if (other.CompareTag("Attacker") && other.gameObject.Equals(_lockTarget.gameObject))
            {
                _lockTarget = null;
            }
        }

        public void UpdateKillCounter()
        {
            _killCount++;
            _killCountText.text = _killCount.ToString();
        }
    }
}
