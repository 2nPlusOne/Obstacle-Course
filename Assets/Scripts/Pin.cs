using System.Collections;
using UnityEngine;

public class Pin : MonoBehaviour, ITaggable, IDamageable<int>
{
    bool isTagged = false;

    [SerializeField] float destroyWaitTime = 1.0f;
    [SerializeField] ParticleSystem poofParticlePrefab;
    [SerializeField] Transform particleSpawnTransform;

    public void TakeDamage(int damageTaken)
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyWaitTime);

        ParticleSystem particles = Instantiate(poofParticlePrefab, particleSpawnTransform.position, particleSpawnTransform.rotation);
        particles.transform.Rotate(90, 0, 0, Space.Self);
        Destroy(gameObject);
    }

    void ITaggable.Tag()
    {
        if (!isTagged)
        {
            isTagged = true;

            MeshRenderer renderer = GetComponentInParent<MeshRenderer>();
            Vector4 pinColor = renderer.materials[0].color;
            renderer.materials[0].color = renderer.materials[1].color;
            renderer.materials[1].color = pinColor;

            EventManager.HitPin();
        }
    }
}
