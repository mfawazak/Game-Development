using UnityEngine;

public class Football_movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;
        
        Vector3 movement = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, -speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, movement.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, movement.z);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Stop
        if (Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Check if the ball is grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}