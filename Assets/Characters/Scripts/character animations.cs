using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Key Z detected");
            animator.SetBool("isWalking", true);
        }
        
        if (!Input.GetKey(KeyCode.W))
        {
            Debug.Log("Key Z not detected");
            animator.SetBool("isWalking", false);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Key S detected");
            animator.SetBool("isBackWalking", true);
        }
        
        if (!Input.GetKey(KeyCode.S))
        {
            Debug.Log("Key S not detected");
            animator.SetBool("isBackWalking", false);
        }
    }
}
    