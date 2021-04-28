using Sirenix.OdinInspector;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    int pinScore = 0;

    [ShowInInspector, ReadOnly]
    int wallScore = 0;

    void IncrementPinScore()
    {
        pinScore++;
        Debug.Log("Pins hit: " + pinScore);
    }

    void IncrementWallScore()
    {
        wallScore++;
        Debug.Log("Walls hit: " + wallScore);
    }

    void OnEnable()
    {
        EventManager.OnCollidedWithPin += IncrementPinScore;
        EventManager.OnCollidedWithWall += IncrementWallScore;
    }

    void OnDisable()
    {
        EventManager.OnCollidedWithPin -= IncrementPinScore;
        EventManager.OnCollidedWithWall -= IncrementWallScore;
    }
}
