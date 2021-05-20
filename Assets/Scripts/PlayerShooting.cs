using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D sourceRigidBody;
    public GameObject bulletPrefab;

    public float speed = 20;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, sourceRigidBody.transform.position, sourceRigidBody.transform.rotation);
            Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            
            // shoot bullet, moving the source in the opposite direction
            bulletRigidBody.AddForce(bullet.transform.up * speed, ForceMode2D.Impulse);
            sourceRigidBody.AddForce(-bullet.transform.up / 3, ForceMode2D.Impulse);
        }
    }
}
