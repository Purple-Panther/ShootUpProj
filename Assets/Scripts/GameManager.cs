using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreStats scoreManager;
    [SerializeField] private GameObject LVL10Boss;
    [SerializeField] public GameObject GameOverScreen;
    [SerializeField] public GameObject MenuScreen;
    [SerializeField] public GameObject DangerScreen; // Adicione a referência ao DangerScreen aqui

    private Player _player;
    private SpawnerManager _enemySpawner;
    private bool _bossSpawned;

    private void Awake()
    {
        scoreManager.ResetScore();
        _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player>();
        _enemySpawner = FindObjectOfType<SpawnerManager>();
    }

    private void Update()
    {
        if (!_bossSpawned && _player.Data.Level >= 10)
        {
            StartCoroutine(HandleBossSpawn());
            _bossSpawned = true;
        }
        GameOver();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private IEnumerator HandleBossSpawn()
    {
    
        yield return new WaitForSeconds(1f);

        if (DangerScreen != null)
        {
            DangerScreen.SetActive(true);
            yield return new WaitForSeconds(4f); 
            DangerScreen.SetActive(false);
        }

        SpawnerManager.KillAllEnemies();
        yield return new WaitForSeconds(5f);
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (LVL10Boss == null)
        {
            return;
        }

        Vector3 spawnPosition = new Vector3(0.02f, 10.08f, 0);
        Instantiate(LVL10Boss, spawnPosition, Quaternion.identity);
        _enemySpawner.SetBossActive(true);
    }

    private void GameOver()
    {
        if (!(_player.Data.Health <= 0)) return;

        GameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        MenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        MenuScreen.SetActive(false);
    }
}
