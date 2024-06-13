using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class LifeManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartContainer;
    public IEntity player;

    private List<GameObject> hearts = new List<GameObject>(); 
    private int playerLife = 100;
    private int lastPlayerHealth; 

    void Start()
    {
        player = FindObjectOfType<Player>(); // Alterado para FindObjectOfType<Player>()

        if (player != null)
        {
            playerLife = (int)player.Data.Health;
            lastPlayerHealth = playerLife;
            AddHearts((int)(playerLife / 20f)); // Use 20f to perform float division
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
            if (currentHealth < lastPlayerHealth && (lastPlayerHealth - currentHealth) >= 20)
            {
                RemoveHeart();
                lastPlayerHealth = currentHealth;
            }
        }
    }




    void AddHearts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(heart);
        }
    }

    void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(heart);
        }
    }
}