using Base;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class Hud : MonoBehaviour
{
    public static Hud Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text fps;
    public Slider lifeBar;
    public Slider expBar;

    [FormerlySerializedAs("damage_popup")] public GameObject damagePopup;

    [FormerlySerializedAs("playerStats")] [SerializeField]
    private Entity player;

    [SerializeField] public ScoreStats scoreStats;
    [SerializeField] private FpsStats fpsStats;

    public TMP_Text levelText;

    private const float UpdateInterval = 1.0f;
    private float _nextUpdateTime = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public static Hud GetOrFind()
    {
        if (Instance != null) return Instance;
        var found = FindFirstObjectByType<Hud>();
        if (found != null)
        {
            Instance = found;
        }
        return Instance;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player.Player>();

        if (player is not null)
        {
            player.Data.OnDataChanged += PlayerHUD;
            PlayerHUD();
        }
        else
        {
            Debug.LogError("Player object not found with tag 'Player'.");
        }
    }

    void Update()
    {
        fps.text = fpsStats.FpsText();
    }


    void PlayerHUD()
    {
        //Score
        scoreText.text = scoreStats.score.ToString();

        //fps
        fps.text = fpsStats.FpsText();

        //Life
        lifeBar.value = player.Data.Health;

        //Xp
        expBar.maxValue = player.Data.ExpToNextLevel;
        expBar.value = player.Data.Exp;

        //Level
        levelText.text = player.Data.Level.ToString();
    }

    private void OnDestroy()
    {
        if (player is not null)
        {
            player.Data.OnDataChanged -= PlayerHUD;
        }
        if (Instance == this) Instance = null;
    }
}