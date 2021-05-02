using Sirenix.OdinInspector;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public int PinScore { get; private set; } = 0;

    [ShowInInspector, ReadOnly]
    int WallScore = 0;

    void IncrementPinScore()
    {
        PinScore++;
        Debug.Log("Pins hit: " + PinScore);
    }

    void IncrementWallScore()
    {
        WallScore++;
        Debug.Log("Walls hit: " + WallScore);
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
