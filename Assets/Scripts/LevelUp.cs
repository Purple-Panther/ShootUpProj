using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour
{
    public GameObject levelUpPanel;
    private Player _player;
    private PowerUpManager _powerUpManager;
    public GameObject background;

    public List<PowerUpBase> availablePowerUps;
    public GameObject powerUpCardPrefab;

    private GameObject _newCard;
    private PowerUpCard _powerUpCard;

    private int _currentLevel;

    public event Action OnGameUnpaused;

    void Start()
    {
        var playerObject = GameObject.FindGameObjectWithTag(Constraints.PlayerTag);
        _powerUpManager = playerObject.GetComponent<PowerUpManager>();
        _player = playerObject.GetComponent<Player>();
        _currentLevel = _player.Data.Level;
        levelUpPanel.SetActive(false);
    }

    void Update()
    {
        if (_player.Data.Level > _currentLevel)
        {
            OpenLevelUpPanel();
            _currentLevel = _player.Data.Level;
        }
    }

    void OpenLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
        RandomCards();
        Time.timeScale = 0f;
    }

    public void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke();
    }

    private void RandomCards()
    {
        foreach (Transform child in background.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, availablePowerUps.Count - 1);
            GameObject newCard = Instantiate(powerUpCardPrefab, background.transform);
            newCard.GetComponent<PowerUpCard>().SetupPowerUpCard(availablePowerUps[randomNumber], this);
        }
    }

    public void ChoosePowerUp(PowerUpBase powerUp)
    {
        if (_powerUpManager is not null)
        {
            _powerUpManager.ActivatePowerUp(powerUp);
            _player.AddPowerUp(powerUp);
        }
        else
            Debug.LogError("PowerUpManager component not found on the player object.");

        CloseLevelUpPanel();
    }
}