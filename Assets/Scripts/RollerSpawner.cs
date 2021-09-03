using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RollerSpawner : MonoBehaviour
{
    [SerializeField]
    MeshRenderer rampRenderer;

    [LabelText("@objectPrefab == null ? \"Object: none\" : \"Object: \" + objectPrefab.name")]
    [SerializeField]
    GameObject objectPrefab;

    [SerializeField, Range(0, 3)]
    float minSpawnGap;

    MeshRenderer objectRenderer;

    [SerializeField, MinMaxSlider(0, 10, showFields: true)]
    Vector2 timeToWaitRange;

    bool canDropObject = true;
    float timeToWait;         // seconds to wait before the next drop
    float rampExtentX;        // Contains the floor's extents along the x and z axes
    float rollerExtentX;      // Contains the drop object's extents along the x and z axes
    Vector2 dropBoundX;       // Contains the minimum and maximum drop location along the x axis
    Vector3 dropPosition;     // The current drop position
    Vector3 lastDropPosition; // The last drop position

    void Start()
    {
        objectRenderer = objectPrefab.GetComponent<MeshRenderer>();

        rampExtentX = rampRenderer.bounds.extents.x;
        rollerExtentX = objectRenderer.bounds.extents.x;
        dropBoundX = new Vector2(-rampExtentX + rollerExtentX, rampExtentX - rollerExtentX);
    }

    void Update()
    {
        if (canDropObject)
        {
            StartCoroutine(DropObject());
            canDropObject = false;
        }
    }

    IEnumerator DropObject()
    {
        timeToWait = Random.Range(timeToWaitRange.x, timeToWaitRange.y);

        if (dropPosition != null)
            lastDropPosition = dropPosition;

        dropPosition = new Vector3(Random.Range(dropBoundX.x, dropBoundX.y), transform.position.y, transform.position.z);
        while (Vector3.Distance(dropPosition, lastDropPosition) < minSpawnGap)
            dropPosition = new Vector3(Random.Range(dropBoundX.x, dropBoundX.y), transform.position.y, transform.position.z);

        yield return new WaitForSeconds(timeToWait);

        //Instantiate(objectPrefab, dropPosition, objectPrefab.transform.rotation);
        GameObject roller = RollerObjectPool.SharedInstance.GetPooledObject();
        if (roller != null)
        {
            roller.transform.position = dropPosition;
            roller.transform.rotation = objectPrefab.transform.rotation;
            Rigidbody rollerRb = roller.GetComponent<Rigidbody>();
            rollerRb.velocity = Vector3.zero;
            roller.SetActive(true);
        }
        canDropObject = true;
    }
}
