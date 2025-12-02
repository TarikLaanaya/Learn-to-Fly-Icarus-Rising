using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float gravity = 9f;
    [SerializeField] private float maxSpeed = 200f;
    [SerializeField] private float speedFactor = 80f;
    [SerializeField] private float deceleration = 1f;
    private float currentSpeed = 0f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] Transform modelTransform;
    private Rigidbody2D rb;
    private float rotationInput;
    [HideInInspector] public bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");

        Debug.DrawRay(transform.position, rb.linearVelocity, Color.green);
    }

    void FixedUpdate()
    {
        if (!isAlive) { rb.linearVelocity = Vector2.zero; return; }

        // --- ROTATION --- //

        Rotate();

        // --- GLIDE MOVEMENT --- //

        GlideMovement();
    }

    void Rotate()
    {
        // Clamp rotation
        if (modelTransform.rotation.z > .7f && rotationInput < 0)
        {
            return;
        }
        else if (modelTransform.rotation.z < -.7f && rotationInput > 0)
        {
            return;
        }

        modelTransform.Rotate(0, 0, -rotationInput * rotationSpeed * Time.deltaTime); // Rotate based on the input * rotation speed
    }

    void GlideMovement()
    {
        float addedSpeed = 0;

        //float angleOfAttack = modelTransform.rotation.z + .7f * 2; // z rotation goes from -0.7 to 0.7 but now it goes from 0 to 2.8

        // Dont add speed when pointed up
        if (modelTransform.rotation.z > 0)
        {
            addedSpeed = 0;
            currentSpeed -= deceleration * Time.deltaTime; // Deceleration (only when pointed up)
        }
        else if (modelTransform.rotation.z < 0)
        {
            addedSpeed = speedFactor * Mathf.Clamp(modelTransform.rotation.z / -0.7f, .5f, 1f); // Reduce speed factor based on how steep the angle is
        }

        currentSpeed += addedSpeed * Time.deltaTime; // Adjust current speed based on the added speed
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // Clamp the speed

        rb.linearVelocity = modelTransform.right * currentSpeed + Vector3.up * -gravity; // Set velocity and add gravity
    }
}
