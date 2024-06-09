using UnityEngine;

[CreateAssetMenu(fileName = "newFpsStats", menuName = "FpsStats")]
public class FpsStats : ScriptableObject
{
    private float _deltaTime = 0.0f;
    private float Fps => 1.0f / _deltaTime;

    public string FpsText()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        return $"{Fps:0.} FPS";
    }
}