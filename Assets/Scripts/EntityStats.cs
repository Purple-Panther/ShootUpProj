using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    public int maxHealth;
    public float health;
    public float baseSpeed;
    public float attackDamage;
    public float attackSpeed;
    public float attackLife;
    public int score;
    public float deltaTime;
    public string fps;

    public GameObject powerUpPrefab; // The power-up prefab to drop
    public float powerUpDropChance; // The chance to drop a power-up (0 to 1)


    public void Start()
    {
        health = maxHealth;
    }

    void Death()
    {
        if (health <= 0)
        {
            if (this.gameObject.tag != "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().AddScore(score);
            }


            if (powerUpPrefab != null && Random.value < powerUpDropChance)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        var fpss = 1.0f / deltaTime;
        fps = $"{fpss:0.} FPS";
    }

    public void TakeDamage(float hp_to_remove)
    {
        health -= hp_to_remove;
        Death();
    }

    void AddScore(int score_to_add)
    {
        score += score_to_add;
    }
}