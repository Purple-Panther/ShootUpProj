using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class LifeManager : MonoBehaviour
{
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public Transform heartContainer;
    public IEntity player;

    private List<GameObject> hearts = new List<GameObject>(); 
    private int playerLife = 100;
    private int lastPlayerHealth; 

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            playerLife = (int)player.Data.Health;
            lastPlayerHealth = playerLife;
            AddHearts(playerLife);
        }
        else
        {
            Debug.LogError("Player object with Player component not found in scene.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            int currentHealth = (int)player.Data.Health;
            if (currentHealth != lastPlayerHealth)
            {
                UpdateHearts(currentHealth);
                lastPlayerHealth = currentHealth;
            }
        }
    }

    void AddHearts(int health)
    {
        int fullHeartCount = health / 20;
        int halfHeartCount = (health % 20) / 10;

        for (int i = 0; i < fullHeartCount; i++)
        {
            GameObject heart = Instantiate(fullHeartPrefab, heartContainer);
            hearts.Add(heart);
        }

        if (halfHeartCount > 0)
        {
            GameObject halfHeart = Instantiate(halfHeartPrefab, heartContainer);
            hearts.Add(halfHeart);
        }
    }

    void UpdateHearts(int currentHealth)
    {
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();
        
        AddHearts(currentHealth);
    }
}