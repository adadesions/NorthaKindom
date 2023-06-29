using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentController : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private List<Transform> _paths;
    [SerializeField] private float _walkSpeed = 1.0f;
    private int _curWayPoint = 0;
    private float _lastTimeNextTarget = 0.0f;
    private float _nextCooldown = 0.5f;
    [SerializeField] private bool _isWalkRandomly = false;
    [SerializeField] private int _maxEnergy = 10;
    private int _energy = 10;
    [SerializeField] private Transform _chargerStation;
    [SerializeField] private TextMeshProUGUI _energyUI;
    private bool _isChase = false;
    [SerializeField] private float _rangeChasing = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (_paths.Count > 0)
        {
            _target = _paths[_curWayPoint];
        }

        _energy = _maxEnergy;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movements();
        ChesingChecking();
    }

    private void ChesingChecking()
    {
        if (!_isChase) return;

        var distance = (transform.position - _target.position).magnitude;
        if (distance > _rangeChasing)
        {
            _isChase = false;
            GoNextWayPoint();
        }
    }

    private void Movements()
    {
        transform.LookAt(_target);
        transform.position =
            Vector3.MoveTowards(transform.position, _target.position, _walkSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isChase) return;
        
        if (other.CompareTag("WayPoint"))
        {
            if (Time.time < _lastTimeNextTarget + _nextCooldown) return;
            _lastTimeNextTarget = Time.time;
            _energy--;
            UpdateEnergyUI();

            if (_energy <= 0)
            {
                GoChargerStation();
                return;
            }
            GoNextWayPoint();
        }
    }

    private void UpdateEnergyUI()
    {
        _energyUI.text = "Energy: " + _energy.ToString() + " / " + _maxEnergy.ToString();
    }

    private void GoChargerStation()
    {
        _target = _chargerStation;
    }

    private void GoNextWayPoint()
    {
        int nextRand = Random.Range(0, _paths.Count);
        if (nextRand == _curWayPoint) nextRand++;
        _curWayPoint = _isWalkRandomly ? nextRand : _curWayPoint + 1;
        
        // if (isRandom)
        // {
        //     _curWayPoint = Random.Range(0, _paths.Count);
        // }
        // else
        // {
        //     _curWayPoint++;    
        // }
        
        if (_curWayPoint >= _paths.Count)
        {
            _curWayPoint = 0;
        }

        _target = _paths[_curWayPoint];
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetIsChasing(bool isChase)
    {
        _isChase = isChase;
    }
}
