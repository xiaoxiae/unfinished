using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D sourceRigidBody;
    public GameObject bulletPrefab;

    public Player player;

    public AudioSource shootingAudio;
    public AudioSource hitSound;
    public AudioSource reloadSound;

    public InputField field;

    private DateTime nextTimeShot;
    private bool reloading;
    
    void Update()
    {
        if (field.isFocused)
            return;
        
        // finished reloading
        if (reloading && nextTimeShot <= DateTime.Now)
        {
            reloading = false;
            player.Magazines -= 1;
            player.CurrentAmmo = player.MaxAmmo;
        }

        // R pressed - start reloading
        // don't reload on full ammo
        if (!reloading && Input.GetKey(KeyCode.R) && player.CurrentAmmo != player.MaxAmmo)
        {
            reloading = true;
            nextTimeShot = DateTime.Now.AddSeconds(player.ReloadDelay);
            reloadSound.Play();
        }
        
        // out of bullets - start reloading
        if (nextTimeShot <= DateTime.Now && player.CurrentAmmo == 0 && player.Magazines != 0)
        {
            reloading = true;
            nextTimeShot = DateTime.Now.AddSeconds(player.ReloadDelay);
            reloadSound.Play();
        }
        
        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
        {
            
            if (nextTimeShot <= DateTime.Now && player.CurrentAmmo > 0)
            {
                float r = Random.Range(-15f, 15f);
                nextTimeShot = DateTime.Now.AddSeconds(player.ShootingDelay);

                player.CurrentAmmo -= 1;

                // steady when shooting
                if (GetComponent<PlayerMovement>().direction == Vector2.zero)
                    r = 0;
                
                shootingAudio.Play();
                
                GameObject bullet = Instantiate(bulletPrefab, sourceRigidBody.transform.position,
                    Quaternion.Euler(0, 0, sourceRigidBody.rotation + r));
                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

                bullet.GetComponent<Bullet>().hitSound = hitSound;

                // shoot bullet, moving the source in the opposite direction
                bulletRigidBody.AddForce(bullet.transform.up * Bullet.Speed, ForceMode2D.Force);
                sourceRigidBody.AddForce(-bullet.transform.up, ForceMode2D.Impulse);
            }
        }
    }
}
