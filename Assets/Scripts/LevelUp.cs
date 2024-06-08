using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject levelUpPanel; 
    public GameObject player; 

    public GameObject Background;

       public List<PowerUpScr> AvailablePowerUps;
        public GameObject PowerUpCardPrefab;

    private EntityStats playerStats; 
    private int currentLevel; 


    void Start()
    {
        RandomCards();
        levelUpPanel.SetActive(false); 
        playerStats = player.GetComponent<EntityStats>();
        currentLevel = playerStats.level; 
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has leveled up
        if (playerStats.level > currentLevel)
        {
            OpenLevelUpPanel();
            currentLevel = playerStats.level;
        }
    }

    void OpenLevelUpPanel()
    {
        levelUpPanel.SetActive(true); 
    }

     public void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }

    void RandomCards()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, AvailablePowerUps.Count);

            GameObject newCard = Instantiate(PowerUpCardPrefab, Background.transform);
            newCard.GetComponent<PowerUpCard>().powerUp = AvailablePowerUps[randomNumber];
            newCard.GetComponent<PowerUpCard>().SetupPowerUpCard(AvailablePowerUps[randomNumber]);
        }
    }
}