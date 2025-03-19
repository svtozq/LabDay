using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float rotationSpeed = 200.0f;
    public float jumpForce = 15f;
    public float fallMultiplier = 4.5f;
    public float lowJumpMultiplier = 2.5f;
    public LayerMask groundMask;
    public Transform groundCheck;
    private Rigidbody rb;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (rb == null)
        {
            Debug.LogError("A Rigidbody component is required on the playerBody!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 forwardMovement = transform.forward * moveVertical * speed;
        Vector3 newPositionVertical = rb.position + forwardMovement * Time.deltaTime;
        rb.MovePosition(newPositionVertical);
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 sideMovement = transform.right * moveHorizontal * speed;
        Vector3 newPositionSide = rb.position + sideMovement * Time.deltaTime;
        rb.MovePosition(newPositionSide);

		float rayLength = 2.1f;
        Debug.DrawRay(groundCheck.position, Vector3.down * rayLength, Color.red); // Visualisation du rayon
        isGrounded = Physics.CheckSphere(groundCheck.position, rayLength, groundMask);

        if (isGrounded)
        {
            Debug.Log("Le joueur est au sol.");
        }
        else
        {
            Debug.Log("Le joueur n'est pas au sol.");
        }

		if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
    private void OnDrawGizmosSelected()
    {
        float rayLength = 2.1f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, rayLength);
    }
}
