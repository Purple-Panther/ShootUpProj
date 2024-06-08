using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] eliteEnemyPrefabs; // array de inimigos de elite
    public float eliteSpawnChance = 0.1f; // chance de spawnar um inimigo de elite
    public List<Transform> spawnPoints;
    public float spawnInterval = 3f;
    public float minSpawnInterval = 0.3f;
    public float difficultyIncreaseRate = 0.95f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.timeSinceLevelLoad + spawnInterval;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= nextSpawnTime)
        {
            SpawnEnemies();
            nextSpawnTime = Time.timeSinceLevelLoad + spawnInterval;
            spawnInterval = Mathf.Max(spawnInterval * difficultyIncreaseRate, minSpawnInterval);
        }
    }

    void SpawnEnemies()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        SpawnEnemyAtPoint(spawnIndex);

        if (Random.value < eliteSpawnChance) // se a chance aleatória for menor que a chance de spawn do inimigo de elite
        {
            SpawnEliteEnemyAtPoint(spawnIndex); // spawnar o inimigo de elite
        }
        else if (Random.value > 0.5f)
        {
            SpawnAdditionalEnemies(spawnIndex);
        }
    }

    void SpawnEnemyAtPoint(int spawnIndex)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    void SpawnEliteEnemyAtPoint(int spawnIndex)
    {
        GameObject eliteEnemyPrefab = eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Length)]; // selecionar um inimigo de elite aleatório
        Instantiate(eliteEnemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity); // spawnar o inimigo de elite
    }

    void SpawnAdditionalEnemies(int initialSpawnIndex)
    {
        for (int i = 1; i <= 2; i++)
        {
            int nextIndex = initialSpawnIndex + i;
            if (nextIndex < spawnPoints.Count)
            {
                SpawnEnemyAtPoint(nextIndex);
            }
        }
    }
}