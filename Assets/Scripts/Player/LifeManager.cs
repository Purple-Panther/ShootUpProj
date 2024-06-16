using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class LifeManager : MonoBehaviour
{
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public Transform heartContainer;
    public IEntity Player;

    private readonly IList<GameObject> _hearts = new List<GameObject>();
    private int _playerLife = 100;
    private int _lastPlayerHealth;

    void Start()
    {
        Player = FindObjectOfType<Player>();

        if (Player != null)
        {
            _playerLife = (int)Player.Data.Health;
            _lastPlayerHealth = _playerLife;
            AddHearts(_playerLife);
        }
        else
        {
            Debug.LogError("Player object with Player component not found in scene.");
        }
    }

    void Update()
    {
        if (Player != null)
        {
            int currentHealth = (int)Player.Data.Health;
            if (currentHealth != _lastPlayerHealth)
            {
                UpdateHearts(currentHealth);
                _lastPlayerHealth = currentHealth;
            }
        }
    }

    private void AddHearts(int health)
    {
        int fullHeartCount = health / 20;
        int halfHeartCount = (health % 20) / 10;

        for (int i = 0; i < fullHeartCount; i++)
        {
            GameObject heart = Instantiate(fullHeartPrefab, heartContainer);
            _hearts.Add(heart);
        }

        if (halfHeartCount > 0)
        {
            GameObject halfHeart = Instantiate(halfHeartPrefab, heartContainer);
            _hearts.Add(halfHeart);
        }
    }

    private void UpdateHearts(int currentHealth)
    {
        foreach (var heart in _hearts)
        {
            Destroy(heart);
        }
        _hearts.Clear();
        
        AddHearts(currentHealth);
    }
}