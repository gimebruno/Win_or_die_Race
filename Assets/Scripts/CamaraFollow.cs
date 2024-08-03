using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto que la cámara debe seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado de la cámara
    public Vector3 offset; // Desplazamiento de la cámara desde el objetivo

    private float initialZ; // Posición inicial en el eje Z

    void Start()
    {
        // Guardar la posición inicial en el eje Z
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        // Mantener la posición Z inicial
        desiredPosition.z = initialZ;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}


