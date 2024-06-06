using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteRangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5.0f;
    private EntityStats enemyStats;
    private float shootInterval;
    private Rigidbody2D rb;

    void Start()
    {
        enemyStats = GetComponent<EntityStats>();
        shootInterval = enemyStats.attackSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (shootInterval <= 0)
        {
            Shoot();
            shootInterval = enemyStats.attackSpeed;
        }
        else
        {
            shootInterval -= Time.deltaTime;
        }
    }

void Update()
{
    // Gira o objeto em torno do seu eixo Z a uma velocidade de 90 graus por segundo
    transform.Rotate(0, 0, 22.5f * Time.deltaTime);
}

void Shoot()
{  
    Debug.Log("Shoot called"); 
    float[] angles = new float[] { 0, 60, 120, 180, 240, 300 }; // Define os ângulos para cada projétil

    for (int i = 0; i < 6; i++) // Altere o valor para o número de projéteis que você quer disparar
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        float angle = angles[i] + transform.eulerAngles.z; // Adiciona a rotação atual do objeto ao ângulo do projétil
        projectile.transform.Rotate(0, 0, angle); // Rotaciona o projétil para o ângulo

        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        if (rbProjectile != null)
        {
            rbProjectile.velocity = projectile.transform.up * projectileSpeed;
        }

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(enemyStats.attackDamage);
            projectileScript.projectileLifeSpan = enemyStats.attackLife;
        }
    }
}
}