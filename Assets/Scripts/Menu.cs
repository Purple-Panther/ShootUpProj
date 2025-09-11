using Manager;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
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

}