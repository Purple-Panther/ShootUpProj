using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    public static Hud Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text fps;
    public Slider lifeBar;
    public Slider expBar;

    EntityStats playerStats;

    public Text levelText;


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
        levelText.text = playerStats.level.ToString();
         if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on player object.");
        }
           PlayerHUD();
    }

    // Update is called once per frame
    void Update()
    {
         if (playerStats != null)
        {
            PlayerHUD();
        }
    }


    void PlayerHUD()
{
    //Score
    scoreText.text = playerStats.score.ToString();
    fps.text = playerStats.fps;
    //Life
    lifeBar.value = playerStats.health;

    //Xp
    expBar.maxValue = playerStats.level * 100;
    expBar.value = playerStats.xp;

    //Level
    levelText.text = playerStats.level.ToString();
}
}