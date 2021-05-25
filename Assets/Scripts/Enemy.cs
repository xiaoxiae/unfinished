﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float CurrentHealth = 100;

    public float FOV = 5;
    
    public LayerMask layerMask;
    
    public Rigidbody2D target;
    private NavMeshAgent agent;

    public float StunDelay = 0.3f;
    public DateTime stunWakeUpTime; // enemy is stunned right after being hit
    
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
        {
            // TODO: some animation?
            Destroy(gameObject);
        }
        
        // if the enemy is stunned, don't move
        if (stunWakeUpTime >= DateTime.Now)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            // if the player is close and we have direct line of sight, attack
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (-(Vector2)transform.position + target.position), FOV, layerMask);
            
            if (hit.rigidbody == target)
                agent.SetDestination(target.position);
            else
                agent.SetDestination(transform.position);
        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent(typeof(Player)) as Player;

        if (player != null)
            player.CurrentHealth -= 1;
    }
}
