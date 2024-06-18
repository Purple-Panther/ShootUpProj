using DefaultNamespace;
using Manager;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ResumeGame();
        }
    }

    public void OnResumeButtonClicked()
    {
        gameManager.ResumeGame();
    }
}