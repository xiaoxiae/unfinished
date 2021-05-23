using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D sourceRigidBody;
    public GameObject bulletPrefab;

    public float speed = 0.001f;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float r = Random.Range(-30f, 30f);
            
            // steady when shooting
            if (GetComponent<PlayerMovement>().direction == Vector2.zero)
                r = 0;

            GameObject bullet = Instantiate(bulletPrefab, sourceRigidBody.transform.position, Quaternion.Euler(0, 0, sourceRigidBody.rotation + r));
            Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            
            if (GetComponent<PlayerMovement>().direction != Vector2.zero)
            {
                sourceRigidBody.rotation -= r;
            }
            
            // shoot bullet, moving the source in the opposite direction
            bulletRigidBody.AddForce(bullet.transform.up * speed, ForceMode2D.Force);
            sourceRigidBody.AddForce(-bullet.transform.up, ForceMode2D.Impulse);
        }
    }
}
