using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage = 17;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy other = collision.gameObject.GetComponent(typeof(Enemy)) as Enemy;

        if (other != null)
            other.CurrentHealth -= Damage;
        
        Destroy(gameObject);
    }
}
