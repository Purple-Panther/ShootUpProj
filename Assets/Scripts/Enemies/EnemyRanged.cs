using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject projectilePrefab; // prefab do projétil
    public float projectileSpeed = 5.0f; // velocidade do projétil
    private EntityStats enemyStats;
private float shootInterval;
private Rigidbody2D rb;

void Start()
{
    enemyStats = GetComponent<EntityStats>();
    shootInterval = enemyStats.attackSpeed;
    rb = GetComponent<Rigidbody2D>();
    Shoot();
}

void FixedUpdate()
{
    rb.velocity = new Vector2(rb.velocity.x, -enemyStats.baseSpeed);

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

void Shoot()
{
    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
    if (rbProjectile != null)
    {
        rbProjectile.velocity = transform.up * -1 * projectileSpeed;
    }

    Projectile projectileScript = projectile.GetComponent<Projectile>();
    if (projectileScript != null)
    {
        projectileScript.Initialize(enemyStats.attackDamage);
        projectileScript.projectileLifeSpan = enemyStats.attackLife;
    }
}
}