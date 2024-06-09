using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public enum ShootMethod
    {
        SingleShot,
        MultiShot
    }

    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Entity entityStats; // Referência para Entity
    public ShootMethod shootMethod = ShootMethod.SingleShot;

    public float attackSpeed_;
    private float nextFireTime;

    void Start()
    {
        entityStats = GetComponent<Entity>();
        attackSpeed_ = entityStats.Data.AttackSpeed;
    }

    void Update()
    {
        entityStats = GetComponent<PlayerMovement>();

        if (Mathf.Approximately(Input.GetAxis("Fire1"), 1) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + attackSpeed_;
        }
    }

    void Shoot()
    {
        switch (shootMethod)
        {
            case ShootMethod.SingleShot:
                FireProjectile(transform.position, Vector2.up * projectileSpeed, Quaternion.identity);
                break;
            case ShootMethod.MultiShot:
                FireProjectile(transform.position, Quaternion.Euler(0, 0, -30) * Vector2.up * projectileSpeed,
                    Quaternion.Euler(0, 0, -30)); // Diagonal esquerda
                FireProjectile(transform.position, Vector2.up * projectileSpeed, Quaternion.identity); // Direto
                FireProjectile(transform.position, Quaternion.Euler(0, 0, 30) * Vector2.up * projectileSpeed,
                    Quaternion.Euler(0, 0, 30)); // Diagonal direita
                break;
        }
    }

    void FireProjectile(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript is not null)
        {
            projectileScript.Initialize(entityStats.Data.AttackDamage);
            projectileScript.projectileLifeSpan = entityStats.Data.AttackLife;
        }

        Destroy(projectile, 2f);
    }
}