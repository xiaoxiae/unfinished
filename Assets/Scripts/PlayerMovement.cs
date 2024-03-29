﻿using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    public Player Player;

    public Vector2 direction;
    public Vector2 mousePos;
    
    public Vector2 scriptDirection;
    public bool scriptSprinting;
    
    public bool Sprinting;

    public AudioSource walkingSound;
    public AudioSource sprintingSound;
    
    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        direction = new Vector2(dx, dy).normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Sprinting = Input.GetKey(KeyCode.LeftShift);
    }
    
    void FixedUpdate()
    {
        // the player gets the final say in the movement
        bool guidedByScript = (direction.magnitude == 0);
        
        Vector2 dir = (guidedByScript ? scriptDirection : direction);
        bool sprinting = (guidedByScript ? scriptSprinting : Sprinting);
        
        rb.AddForce(dir * ((sprinting && Player.CurrentStamina != 0) ? Player.SprintSpeed : Player.Speed), ForceMode2D.Impulse);
        
        if (dir != Vector2.zero)
            if (!walkingSound.isPlaying && !sprintingSound.isPlaying)
            {
                if (sprinting) sprintingSound.Play();
                else walkingSound.Play();
            }

        // if we're sprinting, deplete stamina
        if (sprinting && Player.CurrentStamina != 0 && dir != Vector2.zero)
        {
            Player.CurrentStamina -= 0.6f;
            Player.CurrentStamina = Math.Max(0, Player.CurrentStamina);
        }
        if (!sprinting)
        {
            Player.CurrentStamina += 0.3f;
            Player.CurrentStamina = Math.Min(Player.MaxStamina, Player.CurrentStamina);
        }

        // look over there
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        
    }
}
