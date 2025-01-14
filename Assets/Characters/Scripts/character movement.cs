using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float jumpForce = 15.0f;
    public float rotationSpeed = 200.0f;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 5.0f;
    public LayerMask groundMask; 
    
    private Rigidbody rb;
    private bool isGrounded = true;
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else
        {
            Gizmos.color = Color.red;
        }
    }
}
