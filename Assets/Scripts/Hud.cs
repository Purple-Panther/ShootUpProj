using Misc;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public static Hud Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text fps;
    public Slider lifeBar;
    public Slider expBar;

    [SerializeField]
    private EntityBase playerStats;
    [SerializeField]
    private ScoreStats scoreStats;
    [SerializeField]
    private FpsStats fpsStats;

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
        //TODO: Continuar a abstração, preciso dscobrir como reoslver esse problema comentado
        // playerStats = GameObject.FindGameObjectWithTag("Player")
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        lifeBar.maxValue = playerStats.maxHealth;
        lifeBar.value = playerStats.maxHealth;
        levelText.text = playerStats.level.ToString();
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
    scoreText.text =scoreStats.Score.ToString();;
    fps.text = fpsStats.FpsText();
    //Life
    lifeBar.value = playerStats.health;

    //Xp
    expBar.maxValue = playerStats.level * 100;
    expBar.value = playerStats.xp;

    //Level
    levelText.text = playerStats.level.ToString();
}
}