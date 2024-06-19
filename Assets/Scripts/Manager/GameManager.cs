using System.Collections;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreStats scoreManager;
        [SerializeField] private GameObject LVL10Boss;
        [SerializeField] public GameObject GameOverScreen;
        [SerializeField] public GameObject MenuScreen;
        [SerializeField] public GameObject DangerScreen; // Adicione a referência ao DangerScreen aqui

        private Player _player;
        private SpawnerManager _enemySpawner;
        private CameraManager _cameraManager;
        private bool _bossSpawned;

        private GameObject _boss;
        private BossLvl10 _bossScript;

        private void Awake()
        {
            _boss = Instantiate(LVL10Boss, new Vector3(0.02f, 10.08f, 0), Quaternion.identity);
            _bossScript = _boss.GetComponent<BossLvl10>();
            _boss.SetActive(false);

            scoreManager.ResetScore();
            if (Camera.main is not null) _cameraManager = Camera.main.GetComponent<CameraManager>();
            _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player>();
            _enemySpawner = FindObjectOfType<SpawnerManager>();
            _enemySpawner.SetBossActive(false);
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (_player.Data.Level >= 10)

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

            if (DangerScreen is not null)
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
            if (LVL10Boss is null) return;

            _boss.SetActive(true);
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
}