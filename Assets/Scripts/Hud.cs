using TMPro;
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
    
    [FormerlySerializedAs("damage_popup")] public GameObject damagePopup;

    [FormerlySerializedAs("playerStats")] [SerializeField] private Entity player;
    [SerializeField] public ScoreStats scoreStats;
    [SerializeField] private FpsStats fpsStats;

    public Text levelText;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance is not null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();

        lifeBar.maxValue = player.Data.MaxHealth;
        expBar.value = player.Data.Exp;
        lifeBar.value = player.Data.MaxHealth;
        levelText.text = player.Data.Level.ToString();

        if (player is null)
            Debug.LogError("PlayerStats component not found on player object.");

        PlayerHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (player is not null)
        {
            PlayerHUD();
        }
    }


    void PlayerHUD()
    {
        //Score
        scoreText.text = scoreStats.score.ToString();

        fps.text = fpsStats.FpsText();
        //Life
        lifeBar.value = player.Data.Health;

        //Xp
        expBar.maxValue = player.Data.ExpToNextLevel;
        expBar.value = player.Data.Exp;

        //Level
        levelText.text = player.Data.Level.ToString();
        Debug.Log(scoreStats.score);
    }
}