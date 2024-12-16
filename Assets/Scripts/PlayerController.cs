using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Horizontal movement speed
    public float jumpForce = 10f; // Jump force
    public BoxCollider2D groundCollider; // Ground check collider
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveDir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = groundCollider.IsTouchingLayers(LayerMask.GetMask("Tile"));
    }
}