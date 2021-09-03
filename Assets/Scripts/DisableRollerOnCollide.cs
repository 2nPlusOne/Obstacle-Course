using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRollerOnCollide : MonoBehaviour
{
    [SerializeField]
    GameObject rollerPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Roller")
            collision.gameObject.SetActive(false);
    }
}
