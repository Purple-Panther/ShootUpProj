using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Util;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] eliteEnemyPrefabs;
    public float eliteSpawnChance = 0.1f;
    private readonly List<GameObject> _spawnPoints = new();
    public float spawnInterval = 5f; // Valor aumentado para reduzir a frequência inicial
    public float minSpawnInterval = 1f; // Valor ajustado para o mínimo
    public float difficultyIncreaseRate = 0.95f; // Taxa de aumento de dificuldade reduzida

    private float _nextSpawnTime;
    private bool _bossActive;
    
    private const string InsideAlias = "InsideSpawn";
    private const string OutsideAlias = "OutsideSpawn";

    private void Start()
    {
        _nextSpawnTime = Time.timeSinceLevelLoad + spawnInterval;
        
        _spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("InsideSpawn"));
        _spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("OutsideSpawn"));
        
        ChangeStatusOfSpawns(SpawnPosition.Outside, false);
    }

    public void ChangeStatusOfSpawns(SpawnPosition spawnPosition, bool status)
    {
        var position = spawnPosition is SpawnPosition.Inside ? InsideAlias : OutsideAlias;
        
        foreach (var obj in _spawnPoints.Where(x => x.tag.Equals(position)))
            obj.SetActive(status);
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
        int spawnIndex = Random.Range(0, _spawnPoints.Count(x => x.activeSelf));
        SpawnEnemyAtPoint(spawnIndex);

        if (Random.value < eliteSpawnChance)
            SpawnEliteEnemyAtPoint(spawnIndex);
        else if (Random.value > 0.5f)
            SpawnAdditionalEnemies(spawnIndex);
    }

    private void SpawnEnemyAtPoint(int spawnIndex)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, _spawnPoints.Where(x => x.activeSelf).ToArray()[spawnIndex].transform.position, Quaternion.identity);
    }

    private void SpawnEliteEnemyAtPoint(int spawnIndex)
    {
        GameObject eliteEnemyPrefab = eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Length)];
        Instantiate(eliteEnemyPrefab, _spawnPoints.Where(x => x.activeSelf).ToArray()[spawnIndex].transform.position, Quaternion.identity);
    }

    private void SpawnAdditionalEnemies(int initialSpawnIndex)
    {
        int nextIndex = initialSpawnIndex + 1;
        if (nextIndex < _spawnPoints.Count)
            SpawnEnemyAtPoint(nextIndex);
    }

    public void SetBossActive(bool active)
    {
        _bossActive = active;
    }

    public static void KillAllEnemies()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag(Constraints.EnemyTag))
            Destroy(enemy);
    }
}
