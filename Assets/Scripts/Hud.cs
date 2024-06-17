using System;
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

    [FormerlySerializedAs("playerStats")] [SerializeField]
    private Entity player;

    [SerializeField] public ScoreStats scoreStats;
    [SerializeField] private FpsStats fpsStats;

    public TMP_Text levelText;

    private const float UpdateInterval = 1.0f; // Intervalo de atualização em segundos
    private float _nextUpdateTime = 0f;

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
        // Encontrar o jogador (player) com a tag "Player"
        player = Constraints.PlayerGameObject.GetComponent<Player>();

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
        if (Time.time >= _nextUpdateTime)
        {
            fps.text = fpsStats.FpsText();
            _nextUpdateTime = Time.time + UpdateInterval;
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
    }

    private void OnDestroy()
    {
        if (player is not null)
        {
            player.Data.OnDataChanged -= PlayerHUD;
        }
    }
}