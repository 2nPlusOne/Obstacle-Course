using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject centerOfMassPointer;
    Vector3 com;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        com = centerOfMassPointer.transform.position;
        rb.centerOfMass = com;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rb.centerOfMass, 1);
    }

}
