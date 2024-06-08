using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Hud : MonoBehaviour
{
     public static Hud Instance { get; private set; }
    public TMP_Text scoreText;

    public Slider lifeBar;
    EntityStats playerStats;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        lifeBar.maxValue = playerStats.maxHealth;
        lifeBar.value = playerStats.maxHealth;
        PlayerHUD();
    }

    // Update is called once per frame
      void Update()
    {
        PlayerHUD();
    }


    void PlayerHUD()
    {
         //Score
    scoreText.text = playerStats.score.ToString();

    //Life
    lifeBar.value = playerStats.health;
      
    }
}
