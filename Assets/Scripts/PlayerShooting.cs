using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D sourceRigidBody;
    public GameObject bulletPrefab;

    public Player player;

    private DateTime nextTimeShot;
    private bool reloading = false;
    
    void Update()
    {
        // finished reloading
        if (reloading && nextTimeShot <= DateTime.Now)
        {
            reloading = false;
            player.Magazines -= 1;
            player.CurrentAmmo = player.MaxAmmo;
        }
        
        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
        {
            // start reloading
            if (nextTimeShot <= DateTime.Now && player.CurrentAmmo == 0 && player.Magazines != 0)
            {
                reloading = true;
                nextTimeShot = DateTime.Now.AddSeconds(player.ReloadDelay);
            }
            
            if (nextTimeShot <= DateTime.Now && player.CurrentAmmo > 0)
            {
                float r = Random.Range(-30f, 30f);
                nextTimeShot = DateTime.Now.AddSeconds(player.ShootingDelay);

                player.CurrentAmmo -= 1;

                // steady when shooting
                if (GetComponent<PlayerMovement>().direction == Vector2.zero)
                    r = 0;

                GameObject bullet = Instantiate(bulletPrefab, sourceRigidBody.transform.position,
                    Quaternion.Euler(0, 0, sourceRigidBody.rotation + r));
                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

                // shoot bullet, moving the source in the opposite direction
                bulletRigidBody.AddForce(bullet.transform.up * Bullet.Speed, ForceMode2D.Force);
                sourceRigidBody.AddForce(-bullet.transform.up, ForceMode2D.Impulse);
            }
        }
    }
}
