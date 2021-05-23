using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    public Player Player;
    
    public Vector2 direction;
    public Vector2 mousePos;
    public bool Sprinting;
    
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
        rb.AddForce(direction * ((Sprinting && Player.CurrentStamina != 0) ? Player.SprintSpeed : Player.Speed), ForceMode2D.Impulse);

        // if we're sprinting, deplete stamina
        if (Sprinting && Player.CurrentStamina != 0 && direction != Vector2.zero)
        {
            Player.CurrentStamina -= 0.6f;
            Player.CurrentStamina = Math.Max(0, Player.CurrentStamina);
        }
        if (!Sprinting)
        {
            Player.CurrentStamina += 0.1f;
            Player.CurrentStamina = Math.Min(Player.MaxStamina, Player.CurrentStamina);
        }

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }
}
