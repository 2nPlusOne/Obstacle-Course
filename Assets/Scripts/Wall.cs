using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, ITaggable
{
    bool isTagged = false;

    void ITaggable.Tag()
    {
        if (!isTagged)
        {
            isTagged = true;

            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = new Vector4(.8f, .2f, .2f, 1);

            EventManager.HitWall();
        }
    }
}
