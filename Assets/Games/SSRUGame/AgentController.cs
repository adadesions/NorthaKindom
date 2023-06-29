using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _walkSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
