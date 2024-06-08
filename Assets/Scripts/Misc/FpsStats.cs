using UnityEngine;

namespace Misc
{
    public class FpsStats : ScriptableObject
    {
        public float Fps => 1 / ((Time.unscaledDeltaTime - Fps) * 0.1f);

        public string FpsText() =>$"{Fps:0.} FPS";

    }
}