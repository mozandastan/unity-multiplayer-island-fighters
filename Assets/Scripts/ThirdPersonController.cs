using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Karakterin hareket hýzý
    public float rotationSpeed = 100.0f; // Karakterin dönüþ hýzý

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Karakterin dönüþünü dondur
    }

    void FixedUpdate()
    {
        // Hareket kontrolleri
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);

        // Karakterin dönüþü
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            float turn = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
