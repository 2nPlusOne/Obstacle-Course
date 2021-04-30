using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float rotationSpeed;
    
    Vector3 spinVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        spinVector = new Vector3(0, rotationSpeed * 2, 0);
        rb.AddTorque(spinVector);
        rb.maxAngularVelocity = rotationSpeed;
    }
}
