using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void CollidedWithPin();
    public static event CollidedWithPin OnCollidedWithPin;

    public delegate void CollidedWithWall();
    public static event CollidedWithWall OnCollidedWithWall;

    public static void HitPin() =>
        OnCollidedWithPin?.Invoke();

    public static void HitWall() =>
        OnCollidedWithWall?.Invoke();
}