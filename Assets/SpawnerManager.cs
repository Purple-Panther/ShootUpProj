using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] eliteEnemyPrefabs;
    public float eliteSpawnChance = 0.1f;
    public List<Transform> spawnPoints;
    public float spawnInterval = 3f;
    public float minSpawnInterval = 0.3f;
    public float difficultyIncreaseRate = 0.95f;

    private float _nextSpawnTime;
    private bool _bossActive;

    private void Start()
    {
        _nextSpawnTime = Time.timeSinceLevelLoad + spawnInterval;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad < _nextSpawnTime) return;

        if (!_bossActive)
        {
            SpawnEnemies();
        }

        _nextSpawnTime = Time.timeSinceLevelLoad + spawnInterval;
        spawnInterval = Mathf.Max(spawnInterval * difficultyIncreaseRate, minSpawnInterval);
    }

    private void SpawnEnemies()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        SpawnEnemyAtPoint(spawnIndex);

        if (Random.value < eliteSpawnChance)
            SpawnEliteEnemyAtPoint(spawnIndex);
        else if (Random.value > 0.5f)
            SpawnAdditionalEnemies(spawnIndex);
    }

    private void SpawnEnemyAtPoint(int spawnIndex)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    private void SpawnEliteEnemyAtPoint(int spawnIndex)
    {
        GameObject eliteEnemyPrefab = eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Length)];
        Instantiate(eliteEnemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    private void SpawnAdditionalEnemies(int initialSpawnIndex)
    {
        for (int i = 1; i <= 2; i++)
        {
            int nextIndex = initialSpawnIndex + i;
            if (nextIndex < spawnPoints.Count)
                SpawnEnemyAtPoint(nextIndex);
        }
    }

    public void SetBossActive(bool active)
    {
        _bossActive = active;
    }

    public void KillAllEnemies()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag(Constraints.EnemyTag))
        {
            Destroy(enemy);
        }
    }
}
