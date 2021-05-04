using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class ThirdPersonMovement : MonoBehaviour, ILockableInput
{
    Rigidbody rb;
    SphereCollider sphereCollider;
    Camera cam;
    Transform camT;

    [SerializeField] float torque = 120;
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float maxInputLockSpeed = 2;
    [SerializeField] float groundedCheckDist = 0.1f;

    [SerializeField, ReadOnly] Vector2 inputDir;
    [SerializeField, ReadOnly] Vector3 targetDir;
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
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        radius = sphereCollider.bounds.extents.x;
        speed = rb.velocity.magnitude;
        rb.maxAngularVelocity = maxSpeed / radius;
        minAngularVelocityInputLock = maxInputLockSpeed / radius;

        targetDir = GetTargetDirection();
        float deltaX = targetDir.x * torque;
        float deltaY = targetDir.z * torque;
        //moveVector = new Vector3(deltaX, rb.velocity.y, deltaY);
        torqueVector = new Vector3(deltaY, rb.velocity.y, -deltaX);

        isGrounded = IsGrounded();
        Move(torqueVector);
    }

    Vector3 GetTargetDirection()
    {
        camT = cam.transform;
        Vector3 _forward = camT.forward; _forward.y = 0f; _forward.Normalize();
        Vector3 _right = camT.right; _right.y = 0f; _right.Normalize();

        Vector3 _targetDirection = _forward * inputDir.y + _right * inputDir.x;
        return _targetDirection;
    }

    void Move(Vector3 _torqueVector)
    {
        if (inputDir.magnitude >= 0.2f && isGrounded)
        {
            if (inputLocked && speed <= maxInputLockSpeed)
            {
                rb.maxAngularVelocity = minAngularVelocityInputLock;
                rb.AddTorque(_torqueVector);
            }
            else if (!inputLocked)
            {
                rb.AddTorque(_torqueVector);
                //rb.AddForce(moveVector);
            }
        }
    }

    bool IsGrounded()
    {
        groundedRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(groundedRay, radius + groundedCheckDist))
            return true;

        else return false;
        //Debug.DrawRay(groundedRay.origin, groundedRay.direction, Color.red);
    }

    void OnPlayerMovementPerformed(Vector2 direction)
    {
        inputDir = direction.normalized;
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
