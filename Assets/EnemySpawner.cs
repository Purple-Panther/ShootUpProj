using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public List<Transform> spawnPoints; // Pontos de spawn
    public float spawnInterval = 3f; // Intervalo inicial para spawn
    public float minSpawnInterval = 0.3f; // Intervalo mínimo para spawn
    public float difficultyIncreaseRate = 0.95f; // Taxa de diminuição do intervalo de spawn

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies();
            nextSpawnTime = Time.time + spawnInterval;
            // Aumenta a dificuldade diminuindo o intervalo de spawn, mas não abaixo do mínimo
            spawnInterval = Mathf.Max(spawnInterval * difficultyIncreaseRate, minSpawnInterval);
        }
    }

    void SpawnEnemies()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count); // Escolhe um ponto de spawn aleatório
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);

        // Chance de spawnar inimigos adicionais em pontos de spawn adjacentes
        if (Random.value > 0.5f) // 50% de chance para exemplo
        {
            for (int i = 1; i <= 2; i++) // Tenta spawnar até 2 inimigos adicionais
            {
                int nextIndex = spawnIndex + i;
                if (nextIndex < spawnPoints.Count) // Verifica se o próximo ponto de spawn existe
                {
                    Instantiate(enemyPrefab, spawnPoints[nextIndex].position, Quaternion.identity);
                }
            }
        }
    }
}