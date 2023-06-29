using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentController : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private List<Transform> _paths;
    [SerializeField] private float _walkSpeed = 1.0f;
    private int _curWayPoint = 0;
    private float _lastTimeNextTarget = 0.0f;
    private float _nextCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (_paths.Count > 0)
        {
            _target = _paths[_curWayPoint];
        }
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movements();
    }

    private void Movements()
    {
        transform.LookAt(_target);
        transform.position =
            Vector3.MoveTowards(transform.position, _target.position, _walkSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            if (Time.time < _lastTimeNextTarget + _nextCooldown) return;

            _lastTimeNextTarget = Time.time;
            _curWayPoint++;
            if (_curWayPoint >= _paths.Count)
            {
                _curWayPoint = 0;
            }
            _target = _paths[_curWayPoint];
            
            print("_curWayPoint: " + _curWayPoint.ToString());
            print("Name: " + _target.name);
            print(_paths.Count);
        }
    }
}
