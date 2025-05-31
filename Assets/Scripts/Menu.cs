using DefaultNamespace;
using Manager;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager != null)
                gameManager.ResumeGame();
        }
    }

    public void OnResumeButtonClicked()
    {
        if (gameManager != null)
            gameManager.ResumeGame();
    }

    public void OnExitButtonPressed()
    {
        if (gameManager != null)
            gameManager.ExitGame();
    }

}