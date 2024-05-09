using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Kameranýn takip edeceði hedef (karakter)
    public float distance = 5.0f; // Kamera ile hedef arasýndaki mesafe
    public float height = 2.0f; // Kamera yüksekliði
    public float damping = 5.0f; // Kamera hareket yumuþatma miktarý
    public float rotationDamping = 10.0f; // Kamera dönüþ yumuþatma miktarý
    public float lookAtHeight = 2.0f; // Kameranýn hedefe bakarken yükseklik

    void LateUpdate()
    {
        if (!target)
            return;

        // Hedefin pozisyonunu ve rotasyonunu al
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        // Kameranýn hedefin arkasýna konumlandýrýlmasý
        Vector3 cameraOffset = -(targetRotation * Vector3.forward * distance);
        cameraOffset.y = height;

        // Kameranýn hedefin üstüne bakmasý
        Vector3 lookAtPosition = targetPosition + Vector3.up * lookAtHeight;

        // Kamerayý yumuþak bir þekilde hedefe doðru konumlandýr
        transform.position = Vector3.Lerp(transform.position, targetPosition + cameraOffset, damping * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtPosition - transform.position), rotationDamping * Time.deltaTime);
    }
}
