using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Pusher : MonoBehaviour
{
    Sequence tweenSequence;
    Tween pushWait;
    Tween push;
    Tween pullWait;
    Tween pull;
    float startX;
    float waitTime;

    System.Random randomGen;

    void Start()
    {
        // Initialize the random generator based on the unique instance ID of the game object
        randomGen = new System.Random((int)gameObject.GetInstanceID());

        startX = transform.position.x;

        tweenSequence = DOTween.Sequence();
        waitTime = NextFloat(0, 1f);
        pushWait = transform.DOMoveX(startX, waitTime);
        push = transform.DOMoveX(startX + 5, 1.5f);
        pullWait = transform.DOMoveX(startX + 5, waitTime);
        pull = transform.DOMoveX(startX, 1.5f);
    }

    void Update()
    {
        tweenSequence.Append(pushWait).Append(push).Append(pullWait).Append(pull).SetLoops(int.MaxValue, LoopType.Restart);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //tweenSequence.Pause();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        tweenSequence.Play();
    }
    float NextFloat(float minimum, float maximum)
    {
        return (float)randomGen.NextDouble() * (maximum - minimum) + minimum;
    }
}
