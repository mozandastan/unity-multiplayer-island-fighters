using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Kameran�n takip edece�i hedef (karakter)
    public float distance = 5.0f; // Kamera ile hedef aras�ndaki mesafe
    public float height = 2.0f; // Kamera y�ksekli�i
    public float damping = 5.0f; // Kamera hareket yumu�atma miktar�
    public float rotationDamping = 10.0f; // Kamera d�n�� yumu�atma miktar�
    public float lookAtHeight = 2.0f; // Kameran�n hedefe bakarken y�kseklik

    void LateUpdate()
    {
        if (!target)
            return;

        // Hedefin pozisyonunu ve rotasyonunu al
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        // Kameran�n hedefin arkas�na konumland�r�lmas�
        Vector3 cameraOffset = -(targetRotation * Vector3.forward * distance);
        cameraOffset.y = height;

        // Kameran�n hedefin �st�ne bakmas�
        Vector3 lookAtPosition = targetPosition + Vector3.up * lookAtHeight;

        // Kameray� yumu�ak bir �ekilde hedefe do�ru konumland�r
        transform.position = Vector3.Lerp(transform.position, targetPosition + cameraOffset, damping * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtPosition - transform.position), rotationDamping * Time.deltaTime);
    }
}
