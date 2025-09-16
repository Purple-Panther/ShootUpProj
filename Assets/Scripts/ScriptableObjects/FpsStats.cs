using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "newFpsStats", menuName = "FpsStats")]
    public class FpsStats : ScriptableObject
    {
        private float _deltaTime = 0.0f;
        private float Fps => _deltaTime > 0f ? 1f / _deltaTime : 0f;

        public string FpsText()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            return $"{Fps:0.} FPS";
        }
    }
}