using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float rotationSpeed = 200.0f;
    private Rigidbody rb;
    
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
    }
}