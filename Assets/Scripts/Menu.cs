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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager?.ResumeGame();
        }
    }

    public void OnResumeButtonClicked()
    {
        _gameManager?.ResumeGame();
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
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("InitialScene");
    }
}