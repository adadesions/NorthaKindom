using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private List<Transform> _paths;
    [SerializeField] private float _walkSpeed = 1.0f;
    private int _curWayPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (_paths.Count > 0)
        {
            _target = _paths[0];
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
            print("_curWayPoint: " + _curWayPoint.ToString());
            _curWayPoint++;
            if (_curWayPoint >= _paths.Count)
            {
                _curWayPoint = 0;
            }
            _target = _paths[_curWayPoint];
        }
    }
}
