using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 7f;
    public float deceleration = 3f;
    public float maxSpeed = 7f;
    public float reverseSpeed = 3f;
    public float rotationSpeed = 170f;
    public float normalDriftFactor = 0.95f; // Factor de fricción normal
    public float driftDriftFactor = 0.7f; // Factor de fricción durante el derrape
    public float brakePower = 2f; // Potencia de frenado
    public Rigidbody2D rb; // Referencia al Rigidbody2D

    private float currentSpeed = 0f;
    private float currentDriftFactor;
    private float driftTransitionTime = 0.25f; // Tiempo de transición del derrape
    private float driftTransitionCounter = 0f; // Contador de transición

    private PlayerInput playerInput; // Referencia al script de input

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        currentDriftFactor = driftDriftFactor; // Iniciar con el derrape activado
        playerInput = GetComponent<PlayerInput>(); // Obtener referencia al script de input
    }

    void Update()
    {
        UpdateInput();
    }

    void FixedUpdate()
    {
        ApplyPhysics();
        HandleRotationStabilization();
    }

    void UpdateInput()
    {
        float verticalInput = playerInput.GetVerticalInput();
        float horizontalInput = playerInput.GetHorizontalInput();
        bool isDrifting = playerInput.IsDrifting();
        bool isBraking = playerInput.IsBraking();

        if (verticalInput > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (verticalInput < 0)
        {
            currentSpeed -= acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -reverseSpeed, maxSpeed);

        if (currentSpeed != 0)
        {
            float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, -rotation);
        }

        // Invertir lógica del derrape
        if (isDrifting)
        {
            currentDriftFactor = normalDriftFactor;
            driftTransitionCounter = driftTransitionTime;
        }
        else
        {
            if (driftTransitionCounter > 0)
            {
                driftTransitionCounter -= Time.deltaTime;
                currentDriftFactor = Mathf.Lerp(normalDriftFactor, driftDriftFactor, 1 - (driftTransitionCounter / driftTransitionTime));
            }
            else
            {
                currentDriftFactor = driftDriftFactor;
            }
        }

        if (isBraking)
        {
            rb.drag = brakePower;
        }
        else
        {
            rb.drag = 0.1f;
        }

        // No detener el auto si no hay input vertical
        if (Mathf.Abs(verticalInput) < 0.1f)
        {
            currentSpeed = 0f; // Detener el auto si no se presionan teclas de movimiento
        }
    }

    void ApplyPhysics()
    {
        Vector2 forwardVelocity = transform.up * currentSpeed;
        rb.velocity = forwardVelocity + (rb.velocity - forwardVelocity) * currentDriftFactor;
    }

    void HandleRotationStabilization()
    {
        // Reducir la velocidad angular gradualmente para evitar que el auto gire sin control
        float angularVelocityReduction = 0.95f;
        if (Mathf.Abs(rb.angularVelocity) > 0.1f && playerInput.GetHorizontalInput() == 0)
        {
            rb.angularVelocity *= angularVelocityReduction;
        }
    }
}