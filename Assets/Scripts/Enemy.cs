using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float CurrentHealth = 100;

    public float FOV = 5;

    public LayerMask layerMask;

    public Rigidbody2D self;
    public Rigidbody2D target;
    
    private NavMeshAgent agent;

    public float StunDelay = 0.4f;
    public DateTime stunWakeUpTime; // enemy is stunned right after being hit
    
    // enemy gets aggressive after hit for a few seconds and will follow the player no matter what
    public float AgressiveDuration = 4;
    public bool agressive;
    public DateTime passiveTime;

    public AudioSource deathSound;
    
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
            deathSound.Play();
            Destroy(gameObject);
        }
        
        // the enemies will be agressive forever -- it is more intuitive that way
        //if (passiveTime <= DateTime.Now)
        //{
        //    agressive = false;
        //}
        
        // if the enemy is stunned, don't move
        if (stunWakeUpTime >= DateTime.Now)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            // if the player is close and we have direct line of sight, attack
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (-(Vector2)transform.position + target.position), FOV, layerMask);

            if (hit.rigidbody == target || agressive)
            {
                agent.SetDestination(target.position);
                
                // look over there
                Vector2 lookDirection = self.position - (Vector2) agent.nextPosition;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
                self.rotation = angle;
            }
            else
            {
                agent.SetDestination(transform.position);
            }

        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent(typeof(Player)) as Player;

        if (player != null)
        {
            player.CurrentHealth -= 1;
            if (player.CurrentHealth <= 0)
                Application.LoadLevel(Application.loadedLevel);
        }
    }
}
