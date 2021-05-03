using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Pusher : MonoBehaviour
{
    Sequence tweenSequence;
    Tween wait;
    Tween push;
    Tween pull;
    float startX;

    System.Random randomGen;

    void Start()
    {
        // Initialize the random generator based on the unique instance ID of the game object
        randomGen = new System.Random((int)gameObject.GetInstanceID());

        startX = transform.position.x;

        tweenSequence = DOTween.Sequence();
        wait = transform.DOMoveX(startX, NextFloat(0,1));
        push = transform.DOMoveX(startX + 5, 1);
        pull = transform.DOMoveX(startX, 2);
    }

    void Update()
    {
        tweenSequence.Append(wait).Append(push).Append(pull).SetLoops(int.MaxValue, LoopType.Restart);
    }

    float NextFloat(float minimum, float maximum)
    {
        return (float)randomGen.NextDouble() * (maximum - minimum) + minimum;
    }
}
