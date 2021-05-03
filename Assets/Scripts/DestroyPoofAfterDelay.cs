using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPoofAfterDelay : MonoBehaviour
{
    ParticleSystem particles;

    float delayTime;
    float timer = 0;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        delayTime = particles.main.startLifetime.constantMax;
        //transform.Rotate(90, 0, 0, Space.Self);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayTime)
        {
            Destroy(gameObject);
        }
    }
}
