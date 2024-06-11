using UnityEngine;
using UnityEngine.UI;

public class PowerUpCard : MonoBehaviour
{
    public Text powerUpNameHolder;
    public Text powerUpDescriptionHolder;
    public Image powerUpSpriteHolder;
    private PowerUpBase powerUp;

    private LevelUp levelUpPanel;

    public void SetupPowerUpCard(PowerUpBase powerUp, LevelUp levelUpPanel)
    {
        this.powerUp = powerUp;
        this.levelUpPanel = levelUpPanel;

        powerUpNameHolder.text = powerUp.powerUpName;
        powerUpDescriptionHolder.text = powerUp.powerUpDescription;
        powerUpSpriteHolder.sprite = powerUp.powerUpSprite;
    }

    public void ChoosePowerUp()
    {
        levelUpPanel.ChoosePowerUp(powerUp);
    }
}