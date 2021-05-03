using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagAndDamageOnHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if object is taggable
        ITaggable taggable = collision.collider.GetComponentInChildren<ITaggable>();
        taggable?.Tag();

        // Check if object is damageable
        IDamageable<int> damageable = collision.collider.GetComponentInChildren<IDamageable<int>>();
        damageable?.TakeDamage(1);
    }
}
