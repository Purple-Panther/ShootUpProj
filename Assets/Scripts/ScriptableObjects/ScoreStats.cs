using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newScoreStats", menuName = "ScoreStats")]
public class ScoreStats : ScriptableObject
{
    [FormerlySerializedAs("Score")] public int score = 0;

    public void AddScore(int scoreToAdd) =>
        score += scoreToAdd;
}