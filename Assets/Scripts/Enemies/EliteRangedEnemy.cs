using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteRangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5.0f;
    public float moveSpeed = 2.0f;
    public float stopYPositionLowerBound = 0f; // Limite inferior da posição Y onde o inimigo deve parar
    public float stopYPositionUpperBound = 5f; // Limite superior da posição Y onde o inimigo deve parar
    private EntityStats enemyStats;
    private float shootInterval;
    private Rigidbody2D rb;
    private bool isMoving = true;

    void Start()
    {
        enemyStats = GetComponent<EntityStats>();
        shootInterval = enemyStats.attackSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(0, -moveSpeed);
            if (transform.position.y <= stopYPositionUpperBound && transform.position.y >= stopYPositionLowerBound)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
            }
        }

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
        transform.Rotate(0, 0, 22.5f * Time.deltaTime);
    }

    void Shoot()
    {  
        float[] angles = new float[] { 0, 60, 120, 180, 240, 300 };

        for (int i = 0; i < 6; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            float angle = angles[i] + transform.eulerAngles.z;
            projectile.transform.Rotate(0, 0, angle);

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