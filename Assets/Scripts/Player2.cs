using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
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
    private bool isDrifting = false;
    private float currentDriftFactor;
    private float transitionTime = 0.25f; // Tiempo de transición del derrape a la normalidad
    private float transitionCounter = 0f; // Contador para la transición

    public SpriteRenderer spriteRenderer;
    public Sprite straightSprite;
    public Sprite leftTurnSprite;
    public Sprite rightTurnSprite;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        currentDriftFactor = normalDriftFactor;
    }

    void Update()
    {
        HandleInput();
        UpdateSprite();
    }

    void FixedUpdate()
    {
        ApplyPhysics();
        HandleRotationStabilization();
    }

    void HandleInput()
    {
        bool isAccelerating = false;
        bool isReversing = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
            isAccelerating = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed -= acceleration * Time.deltaTime;
            isReversing = true;
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -reverseSpeed, maxSpeed);

        float rotation = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation = -rotationSpeed * Time.deltaTime;
        }

        transform.Rotate(Vector3.forward, rotation);

        if (Input.GetKey(KeyCode.RightShift))
        {
            isDrifting = true;
            currentDriftFactor = driftDriftFactor;
        }
        else
        {
            if (isDrifting)
            {
                transitionCounter = transitionTime;
            }
            isDrifting = false;
        }

        if (!isDrifting)
        {
            if (transitionCounter > 0)
            {
                transitionCounter -= Time.deltaTime;
                currentDriftFactor = Mathf.Lerp(driftDriftFactor, normalDriftFactor, 1 - (transitionCounter / transitionTime));
            }
            else
            {
                currentDriftFactor = normalDriftFactor;
            }
        }

        if (Input.GetKey(KeyCode.RightControl))
        {
            rb.drag = brakePower;
        }
        else
        {
            rb.drag = 0.1f;
        }

        if (!isAccelerating && !isReversing)
        {
            currentSpeed = 0f; // Detener el auto si no se presionan teclas de movimiento
        }
    }

    void ApplyPhysics()
    {
        Vector2 forwardVelocity = transform.up * currentSpeed;
        rb.velocity = forwardVelocity + (rb.velocity - forwardVelocity) * currentDriftFactor;
    }

    void UpdateSprite()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.sprite = leftTurnSprite;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.sprite = rightTurnSprite;
        }
        else
        {
            spriteRenderer.sprite = straightSprite;
        }
    }

    void HandleRotationStabilization()
    {
        // Reducir la velocidad angular gradualmente para evitar que el auto gire sin control
        float angularVelocityReduction = 0.95f;
        if (Mathf.Abs(rb.angularVelocity) > 0.1f && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.angularVelocity *= angularVelocityReduction;
        }
    }
}




