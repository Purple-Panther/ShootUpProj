using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCard : MonoBehaviour
{
    public PowerUpScr powerUp;

    public Text powerUpNameHolder;
    public Text powerUpDescriptionHolder;
    public Image powerUpSpriteHolder;

    LevelUp levelUpPanel;

    // Start is called before the first frame update
    void Start()
    {
        // If levelUpPanel is not assigned in the inspector, find it
        levelUpPanel = FindObjectOfType<LevelUp>();

        SetupPowerUpCard(powerUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPowerUpCard(PowerUpScr powerUp)
    {
        powerUpNameHolder.text = powerUp.powerUpName;
        powerUpDescriptionHolder.text = powerUp.powerUpDescription;
        powerUpSpriteHolder.sprite = powerUp.powerUpSprite;
    }

    public void ChoosePowerUp()
    {
        Debug.Log("You have chosen the power up: " + powerUp.powerUpName);

        levelUpPanel.CloseLevelUpPanel(); 
    }
}