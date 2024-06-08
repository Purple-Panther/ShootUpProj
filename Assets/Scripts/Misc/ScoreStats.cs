using TMPro;
using UnityEngine;

public class ScoreStats : ScriptableObject
{
    public int Score { get; private set; }

    public void AddScore(int scoreToAdd) =>
        Score += scoreToAdd;
}