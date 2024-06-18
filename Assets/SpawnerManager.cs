using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] eliteEnemyPrefabs;
    public float eliteSpawnChance = 0.1f;
    public List<Transform> spawnPoints;
    public float spawnInterval = 5f; // Valor aumentado para reduzir a frequência inicial
    public float minSpawnInterval = 1f; // Valor ajustado para o mínimo
    public float difficultyIncreaseRate = 0.95f; // Taxa de aumento de dificuldade reduzida

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
        // Limitar a quantidade de inimigos adicionais para 1 por chamada
        int nextIndex = initialSpawnIndex + 1;
        if (nextIndex < spawnPoints.Count)
            SpawnEnemyAtPoint(nextIndex);
    }

    public void SetBossActive(bool active)
    {
        _bossActive = active;
    }

    public static void KillAllEnemies()
    {
        foreach (var enemy in Constraints.EnemiesGameObjects)
        {
            Destroy(enemy);
        }
    }
}
