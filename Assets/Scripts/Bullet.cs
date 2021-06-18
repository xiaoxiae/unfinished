using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static float Speed = 80;
    public float Damage = 17;

    public AudioSource hitSound;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy other = collision.gameObject.GetComponent(typeof(Enemy)) as Enemy;

        if (other != null)
        {
            other.CurrentHealth -= Damage;
            other.stunWakeUpTime = DateTime.Now.AddSeconds(other.StunDelay);
            other.passiveTime = DateTime.Now.AddSeconds(other.AgressiveDuration);
            other.agressive = true;
            
            if (other.CurrentHealth > 0)
                hitSound.Play();
        }
        
        Destroy(gameObject);
    }
}
