using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour, ITaggable, IDamageable<int>
{
    bool isTagged = false;

    public void TakeDamage(int damageTaken)
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void ITaggable.Tag()
    {
        if (!isTagged)
        {
            isTagged = true;

            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Vector4 pinColor = renderer.materials[0].color;
            renderer.materials[0].color = renderer.materials[1].color;
            renderer.materials[1].color = pinColor;

            EventManager.HitPin();
        }
    }
}
