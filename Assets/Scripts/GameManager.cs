using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private ScoreStats scoreManager;
        [SerializeField]
        private GameObject LVL10Boss;
        [SerializeField]
        public GameObject GameOverScreen;

        private Player _player;
        private SpawerManager _enemySpawner;
        private bool _bossSpawned;

        private void Awake()
        {
            scoreManager.ResetScore();
            _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player>();
            _enemySpawner = FindObjectOfType<SpawerManager>();
        }

        private void Update()
        {
            if (!_bossSpawned && _player.Data.Level >= 10)
            {
                StartCoroutine(HandleBossSpawn());
                _bossSpawned = true;
            }
            GameOver(); 
        }

        private IEnumerator HandleBossSpawn()
        {
            _enemySpawner.KillAllEnemies();
            yield return new WaitForSeconds(5f);
            SpawnBoss();
        }

        private void SpawnBoss()
        {
            if (LVL10Boss == null)
            {
                Debug.LogError("LVL10Boss prefab is not assigned.");
                return;
            }

            Vector3 spawnPosition = new Vector3(0.02f, 10.08f, 0);
            Instantiate(LVL10Boss, spawnPosition, Quaternion.identity);
            _enemySpawner.SetBossActive(true);
        }

        public void GameOver()
        {
            if (_player.Data.Health <= 0)
            {
                GameOverScreen.gameObject.SetActive(true);
                Time.timeScale = 0; 
            }
        }
    }
}