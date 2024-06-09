using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private ScoreStats scoreManager;

        private void Awake()
        {
            scoreManager.ResetScore();
        }
    }
}