using DefaultNamespace;
using Manager;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager?.ResumeGame();
        }
    }

    public void OnResumeButtonClicked()
    {
        gameManager?.ResumeGame();
    }

    public void OnExitButtonPressed()
    {
        if (gameManager is not null)
            GameManager.ExitGame();
    }

}