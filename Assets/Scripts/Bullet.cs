using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static float Speed = 80;
    public float Damage = 17;
        
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy other = collision.gameObject.GetComponent(typeof(Enemy)) as Enemy;

        if (other != null)
        {
            other.CurrentHealth -= Damage;
            other.stunWakeUpTime = DateTime.Now.AddSeconds(other.StunDelay);
        }
        
        Destroy(gameObject);
    }
}
