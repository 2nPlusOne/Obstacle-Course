using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sphereCollider;

    [SerializeField] float torque;
    //[SerializeField] float maxAngularVelocity;
    [SerializeField] float maxSpeed;
    [SerializeField, ReadOnly] Vector2 inputDir;
    [SerializeField, ReadOnly] float speed;
    [SerializeField, ReadOnly] float radius;
    //Vector3 moveVector;
    Vector3 torqueVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaX = inputDir.x * torque;
        float deltaY = inputDir.y * torque;
        //moveVector = new Vector3(deltaX, rb.velocity.y, deltaY);
        torqueVector = new Vector3(deltaY, rb.velocity.y, -deltaX);

        radius = sphereCollider.bounds.extents.x;
        speed = rb.velocity.magnitude;
        //speed = rb.angularVelocity.magnitude * radius; // Speed can also be calculated this way, assuming no slip and constant ground contact

        rb.maxAngularVelocity = maxSpeed / radius;

        if (inputDir != Vector2.zero)
        {
            rb.AddTorque(torqueVector);
            //rb.AddForce(moveVector);
        }
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
}
