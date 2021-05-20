using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    public Rigidbody2D rb;
    public Camera cam;

    public Vector2 direction;
    public Vector2 mousePos;
    
    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        direction = new Vector2(dx, dy).normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate()
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
