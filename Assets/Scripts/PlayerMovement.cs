using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour, ILockableInput
{
    Rigidbody rb;
    SphereCollider sphereCollider;

    [SerializeField] float torque;
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float minSpeedInputLock = 2;
    [SerializeField] float groundedCheckDist = 0.1f;

    [SerializeField, ReadOnly] Vector2 inputDir;
    [SerializeField, ReadOnly] float speed;
    [SerializeField, ReadOnly] float radius;
    [SerializeField, ReadOnly] bool isGrounded;
    [SerializeField, ReadOnly] bool inputLocked = false; // Whether the player can control movement

    //Vector3 moveVector;
    Vector3 torqueVector;
    Ray groundedRay;
    float minAngularVelocityInputLock;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        float deltaX = inputDir.x * torque;
        float deltaY = inputDir.y * torque;
        //moveVector = new Vector3(deltaX, rb.velocity.y, deltaY);
        torqueVector = new Vector3(deltaY, rb.velocity.y, -deltaX);

        radius = sphereCollider.bounds.extents.x;
        speed = rb.velocity.magnitude;

        rb.maxAngularVelocity = maxSpeed / radius;
        minAngularVelocityInputLock = minSpeedInputLock / radius;

        HandleGrounded();
        Move();
    }

    private void Move()
    {
        if (inputDir != Vector2.zero && isGrounded)
        {
            if (inputLocked && speed <= minSpeedInputLock)
            {
                rb.maxAngularVelocity = minAngularVelocityInputLock;
                rb.AddTorque(torqueVector);
            }
            else if (!inputLocked)
            {
                rb.AddTorque(torqueVector);
                //rb.AddForce(moveVector);
            }
        }
    }

    private void HandleGrounded()
    {
        // Check if grounded
        groundedRay = new Ray(transform.position, -Vector3.up);
        isGrounded = false;
        if (Physics.Raycast(groundedRay, radius + groundedCheckDist))
            isGrounded = true;
        //Debug.DrawRay(groundedRay.origin, groundedRay.direction, Color.red);
    }

    void OnPlayerMovementPerformed(Vector2 direction)
    {
        inputDir = direction;
    }

    void OnPlayerMovementCanceled()
    {
        inputDir = Vector2.zero;
    }

    void OnEnable()
    {
        // Subscribe to movement events
        InputManager.OnPlayerMovementPerformed += OnPlayerMovementPerformed;
        InputManager.OnPlayerMovementCanceled += OnPlayerMovementCanceled;
    }

    void OnDisable()
    {
        // Un-subscribe to movement events
        InputManager.OnPlayerMovementPerformed -= OnPlayerMovementPerformed;
        InputManager.OnPlayerMovementCanceled -= OnPlayerMovementCanceled;
    }

    void ILockableInput.LockInput()
    {
        inputLocked = true;
    }

    void ILockableInput.UnlockInput()
    {
        inputLocked = false;
    }
}
