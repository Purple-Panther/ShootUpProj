using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject player;
    public GameObject background;

    public List<PowerUpBase> availablePowerUps;
    public GameObject powerUpCardPrefab;

    private Entity playerStats;
    private int currentLevel;

    void Start()
    {
        playerStats = player.GetComponent<Entity>();
        currentLevel = playerStats.Data.Level;
        levelUpPanel.SetActive(false);
    }

    void Update()
    {
        if (playerStats.Data.Level > currentLevel)
        {
            OpenLevelUpPanel();
            currentLevel = playerStats.Data.Level;
        }
    }

    void OpenLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
        RandomCards();
    }

    public void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }

    void RandomCards()
    {
        foreach (Transform child in background.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, availablePowerUps.Count);
            GameObject newCard = Instantiate(powerUpCardPrefab, background.transform);
            newCard.GetComponent<PowerUpCard>().SetupPowerUpCard(availablePowerUps[randomNumber], this);
        }
    }

    public void ChoosePowerUp(PowerUpBase powerUp)
    {
        PowerUpManager powerUpManager = player.GetComponent<PowerUpManager>();
        if (powerUpManager is not null)
        {
            powerUpManager.ActivatePowerUp(powerUp);
        }
        else
        {
            Debug.LogError("PowerUpManager component not found on the player object.");
        }

        CloseLevelUpPanel();
    }
}