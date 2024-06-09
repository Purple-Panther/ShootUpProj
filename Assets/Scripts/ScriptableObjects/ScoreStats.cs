using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newScoreStats", menuName = "ScoreStats")]
public class ScoreStats : ScriptableObject
{
    [FormerlySerializedAs("Score")] public int score;

    public void AddScore(int scoreToAdd) => score += scoreToAdd;
    public void ResetScore() => score = 0;
}