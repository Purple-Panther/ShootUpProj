using UnityEngine;
using UnityEngine.UI;

public class PowerUpCard : MonoBehaviour
{
    public Text nameHolder;
    public Text descriptionHolder;
    public Image spriteHolder;
    private PowerUpBase _powerUp;

    private LevelUp _levelUpPanel;

    public void SetupPowerUpCard(PowerUpBase powerUp, LevelUp levelUpPanel)
    {
        _powerUp = powerUp;
        _levelUpPanel = levelUpPanel;

        nameHolder.text = powerUp.puName;
        descriptionHolder.text = powerUp.description;
        spriteHolder.sprite = powerUp.sprite;
    }

    public void ChoosePowerUp()
    {
        _levelUpPanel.ChoosePowerUp(_powerUp);
    }
}