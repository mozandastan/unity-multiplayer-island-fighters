using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 100.0f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movementVec = Vector3.zero;
    private Quaternion rotateQuat = Quaternion.identity;

    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        // Movement
        float verticalInput = Input.GetAxis("Vertical");

        if(verticalInput != 0)
        {
            movementVec = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movementVec);

            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("MoveBool", true);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetBool("MoveBool", false);
            }
        }


        // Rotate
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            float turn = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
            rotateQuat = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rotateQuat);
        }
    }
}
