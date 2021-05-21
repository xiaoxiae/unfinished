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

        Sprinting = Input.GetKeyDown(KeyCode.LeftShift);
    }
    
    void FixedUpdate()
    {
        rb.AddForce(direction * (Sprinting ? Player.SprintSpeed : Player.Speed), ForceMode2D.Impulse);

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
