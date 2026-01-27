using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
    }

    public void OnResumeButtonClicked()
    {
        _gameManager?.ResumeGame();
    }
    
    public void ContinueEndlessMode()
    {
        _gameManager?.ContinueEndlessMode();
    }

    public void OnExitButtonPressed()
    {
        if (_gameManager is not null)
            GameManager.ExitGame();
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("InitialScene");
    }
}