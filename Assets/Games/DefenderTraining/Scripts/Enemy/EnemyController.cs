using System;
using Games.DefenderTraining.Scripts.Managers;
using UnityEngine;

namespace Games.DefenderTraining.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 5.0f;
        private float _dv = 0.0f;
        private Transform _target;
        private int _curCheckpoint = 0;
        private PathManager _pathManager;
        private Vector3 _direction;

        // Start is called before the first frame update
        void Start()
        {
            _pathManager = PathManager.Instance;
        }

        private void FindPath()
        {
            if (_target) return;

            try
            {
                _target = _pathManager.Paths[_curCheckpoint];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Destroy(gameObject);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            Movements();
        }

        private void Movements()
        {
            if (!_target)
            {
                FindPath();
            }
            _dv = Time.deltaTime * _walkSpeed;
            var enemyPosition = transform.position;
            _direction = _target.position - enemyPosition;
            var newPosition = Vector3.MoveTowards(enemyPosition, _target.position, _dv);
            transform.position = newPosition;
            // transform.LookAt(_target, Vector3.up);
            var newRotation = Quaternion.LookRotation(-_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _walkSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Path"))
            {
                _target = null;
                _curCheckpoint++;
                FindPath();
            }
        }
    }
}
