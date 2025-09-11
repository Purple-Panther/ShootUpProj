using System.Collections;
using System.Xml;
using Enemies.Boss;
using ScriptableObjects;
using UnityEngine;
using Util;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreStats scoreManager;
        [SerializeField] private GameObject LVL10Boss;
        [SerializeField] public GameObject GameOverScreen;
        [SerializeField] public GameObject MenuScreen;
        [SerializeField] public GameObject DangerScreen;

        private Player.Player _player;
        private SpawnerManager _enemySpawner;
        private CameraManager _cameraManager;
        private bool _bossSpawned;

        private GameObject _boss;
        private BossLvl10 _bossScript;

        private GameObject[] _insideBoundaries;
        private GameObject _insideDeadLine;

        private void Awake()
        {
            _boss = Instantiate(LVL10Boss, new Vector3(0.02f, 21.31f, 0), Quaternion.identity);
            _bossScript = _boss.GetComponent<BossLvl10>();
            _boss.SetActive(false);

            scoreManager.ResetScore();
            if (Camera.main is not null) _cameraManager = Camera.main.GetComponent<CameraManager>();
            _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player.Player>();
            _enemySpawner = FindFirstObjectByType<SpawnerManager>();
            _enemySpawner.SetBossActive(false);
            _insideBoundaries = GameObject.FindGameObjectsWithTag(Constraints.InsideBoundariesTag);
            _insideDeadLine = GameObject.FindGameObjectWithTag(Constraints.InsideDeadLineTag);
        }

        private void Update()
        {
            if (!_bossSpawned && _player.Data.Level >= 10)
            {
                StartCoroutine(HandleBossSpawn());
                _bossSpawned = true;
                DisableInsideBoundaries();
            }

            if (_player.Data.Health <= 0)
                GameOver();

            if (Input.GetKeyDown(KeyCode.Escape))
                PauseGame();
            
        }
        
        private void DisableInsideBoundaries()
        {
            foreach (var gameObject in _insideBoundaries)
                gameObject.SetActive(false);
            
            _insideDeadLine.SetActive(false);
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
            GameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            MenuScreen.SetActive(true);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            MenuScreen.SetActive(false);
        }

        public static void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

    }
}