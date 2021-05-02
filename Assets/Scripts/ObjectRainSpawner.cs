using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectRainSpawner : MonoBehaviour
{
    [SerializeField]
    MeshRenderer floorRenderer;

    [LabelText("@objectPrefab == null ? \"Object: none\" : \"Object: \" + objectPrefab.name")]
    [SerializeField]
    GameObject objectPrefab;

    [SerializeField, Range(0f, 100f)]
    float dropHeight = 100;

    MeshRenderer objectRenderer;

    [SerializeField, MinMaxSlider(0, 10, showFields: true)]
    Vector2 timeToWaitRange;

    bool canDropObject = true;
    float timeToWait;       // seconds to wait before the next drop
    Vector2 floorExtent;    // Contains the floor's extents along the x and z axes
    Vector2 objectExtent;   // Contains the drop object's extents along the x and z axes
    Vector2 dropBoundX;     // Contains the minimum and maximum drop location along the x axis
    Vector2 dropBoundZ;     // Contains the minimum and maximum drop location along the z axis
    Vector3 dropPosition;   // The final drop position


    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = objectPrefab.GetComponent<MeshRenderer>();

        floorExtent = new Vector2(floorRenderer.bounds.extents.x, floorRenderer.bounds.extents.z);
        objectExtent = new Vector2(objectRenderer.bounds.extents.x, objectRenderer.bounds.extents.z);
        dropBoundX = new Vector2(-floorExtent.x + objectExtent.x, floorExtent.x - objectExtent.x);
        dropBoundZ = new Vector2(-floorExtent.y + objectExtent.y, floorExtent.y - objectExtent.y);
    }

    // Update is called once per frame
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
        dropPosition = new Vector3(Random.Range(dropBoundX.x, dropBoundX.y), dropHeight, Random.Range(dropBoundZ.x, dropBoundZ.y));

        yield return new WaitForSeconds(timeToWait);

        //Debug.Log("Drop object at: " + dropPosition);
        Instantiate(objectPrefab, dropPosition, objectPrefab.transform.rotation);
        canDropObject = true;
    }
}
