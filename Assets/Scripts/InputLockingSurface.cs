using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLockingSurface : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ILockableInput lockableInput = other.GetComponent<ILockableInput>();
        lockableInput?.LockInput();
    }

    void OnTriggerExit(Collider other)
    {
        ILockableInput lockableInput = other.GetComponent<ILockableInput>();
        lockableInput?.UnlockInput();
    }
}
