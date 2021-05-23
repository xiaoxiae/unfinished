using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float CurrentHealth = 100;

    public float FOV = 5;
    
    public Transform target;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
            Destroy(gameObject);
        
        if ((transform.position - target.position).magnitude < FOV)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
    }
}
