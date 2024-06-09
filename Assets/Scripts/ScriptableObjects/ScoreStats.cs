using UnityEngine;

[CreateAssetMenu(fileName = "newScoreStats", menuName = "ScoreStats")]
public class ScoreStats : ScriptableObject
{
    public int Score { get; private set; } = 0;

    public void AddScore(int scoreToAdd) =>
        Score += scoreToAdd;
}