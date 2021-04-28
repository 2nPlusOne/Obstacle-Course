using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHitter : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        ITaggable taggable = collision.collider.GetComponent<ITaggable>();
        taggable?.Tag();

        IDamageable<int> damageable = collision.collider.GetComponent<IDamageable<int>>();
        damageable?.TakeDamage(1);
    }
}
