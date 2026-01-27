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
    private float _displayedScore;

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
        
        if (scoreStats != null)
            _displayedScore = scoreStats.score;
    }

    void Update()
    {
        fps.text = fpsStats.FpsText();

        if (scoreStats != null)
        {
            if (_displayedScore < scoreStats.score)
            {
                float diff = scoreStats.score - _displayedScore;
                float increment = Mathf.Max(diff * 5f * Time.deltaTime, 10f * Time.deltaTime); 
                _displayedScore += increment;
                
                if (_displayedScore > scoreStats.score) _displayedScore = scoreStats.score;
            }
            else
            {
                _displayedScore = scoreStats.score;
            }
            
            scoreText.text = Mathf.FloorToInt(_displayedScore).ToString();
        }
    }


    void PlayerHUD()
    {
        fps.text = fpsStats.FpsText();

        lifeBar.value = player.Data.Health;

        expBar.maxValue = player.Data.ExpToNextLevel;
        expBar.value = player.Data.Exp;

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