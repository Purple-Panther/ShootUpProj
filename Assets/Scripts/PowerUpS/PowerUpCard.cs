using UnityEngine;
using UnityEngine.UI;

public class PowerUpCard : MonoBehaviour
{
    public PowerUpScr powerUp;
    public Text powerUpNameHolder;
    public Text powerUpDescriptionHolder;
    public Image powerUpSpriteHolder;

    private LevelUp _levelUpPanel;

    public void SetupPowerUpCard(PowerUpScr powerUp, LevelUp levelUpPanel)
    {
        this.powerUp = powerUp;
        this._levelUpPanel = levelUpPanel;

        powerUpNameHolder.text = powerUp.powerUpName;
        powerUpDescriptionHolder.text = powerUp.powerUpDescription;
        powerUpSpriteHolder.sprite = powerUp.powerUpSprite;
    }

    public void ChoosePowerUp()
    {
        Debug.Log("You have chosen the power up: " + powerUp.powerUpName);
        _levelUpPanel.ChoosePowerUp(powerUp);
    }
}
