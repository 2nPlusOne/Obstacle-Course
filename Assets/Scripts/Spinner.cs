using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    SpinDirection spinDirection;

    Vector3 spinVector;

    enum SpinDirection { Clockwise = 1, CounterClockwise = -1 }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        spinVector = new Vector3(0, rotationSpeed * (int)spinDirection * 2, 0);
        rb.AddTorque(spinVector);
        rb.maxAngularVelocity = rotationSpeed;
    }
}
