using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class EyeAgentDetector : MonoBehaviour
{
    private AgentController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<AgentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _controller.SetTarget(other.transform);
            _controller.SetIsChasing(true);
        }
    }
}
