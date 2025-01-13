using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float jumpForce = 10.0f;
    
    public float rotationSpeed = 200.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public LayerMask groundLayer; 
    public Transform groundCheck;
    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = true; 
    public float groundCheckRadius = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 forwardMovement = transform.forward * moveVertical * speed;
        Vector3 newPosition = rb.position + forwardMovement * Time.deltaTime;
        rb.MovePosition(newPosition);
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (isGrounded && !canJump)
        {
            canJump = true;
        }
        
        if (isGrounded && canJump && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
