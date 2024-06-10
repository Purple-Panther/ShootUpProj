using System.Collections.Generic;
using DefaultNamespace.PowerUpS;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject player;
    public GameObject background;

    public List<PowerUpScr> availablePowerUps;
    public GameObject powerUpCardPrefab;

    private Entity playerStats;
    private int currentLevel;

    // Dicionário para mapear nomes de power-ups para valores do enum
    private readonly Dictionary<string, PowerUpType> powerUpTypeMap = new Dictionary<string, PowerUpType>
    {
        { "MultiShot", PowerUpType.MultiShot },
        { "MachineGun", PowerUpType.MachineGun }
    };

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

    public void ChoosePowerUp(PowerUpScr powerUp)
    {
        PowerUpType parsedPowerUpType;

        if (powerUpTypeMap.TryGetValue(powerUp.powerUpName.Replace(" ", ""), out parsedPowerUpType)) 
        {
            PUManager puManagerComponent = player.GetComponent<PUManager>();
            if (puManagerComponent != null)
            {
                puManagerComponent.ActivatePowerUp(parsedPowerUpType);
            }
            else
            {
                Debug.LogError("PowerUp component not found on the player object.");
            }
        }
        else
        {
            Debug.LogError($"PowerUpType '{powerUp.powerUpName}' not found.");
        }

        CloseLevelUpPanel();
    }

}
