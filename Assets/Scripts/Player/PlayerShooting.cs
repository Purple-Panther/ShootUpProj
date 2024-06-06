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
    public EntityStats entityStats; // Referência para entityStats
    public ShootMethod shootMethod = ShootMethod.SingleShot;

    public float attackSpeed_;

    private float nextFireTime;

    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        attackSpeed_ = entityStats.attackSpeed;
        Debug.Log("PlayerShooting started. Initial attack speed: " + attackSpeed_);
    }

    void Update()
    {
        entityStats = GetComponent<EntityStats>(); 

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Debug.Log("Space key pressed. Firing projectile.");
            Shoot();
            nextFireTime = Time.time + attackSpeed_;
            Debug.Log("Next fire time: " + nextFireTime);
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot method: " + shootMethod);
        switch (shootMethod)
        {
            case ShootMethod.SingleShot:
                FireProjectile(transform.position, Vector2.up * projectileSpeed, Quaternion.identity);
                break;
            case ShootMethod.MultiShot:
                FireProjectile(transform.position, Quaternion.Euler(0, 0, -30) * Vector2.up * projectileSpeed, Quaternion.Euler(0, 0, -30)); // Diagonal esquerda
                FireProjectile(transform.position, Vector2.up * projectileSpeed, Quaternion.identity); // Direto
                FireProjectile(transform.position, Quaternion.Euler(0, 0, 30) * Vector2.up * projectileSpeed, Quaternion.Euler(0, 0, 30)); // Diagonal direita
                break;
        }
    }

    void FireProjectile(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(entityStats.attackDamage);
            projectileScript.projectileLifeSpan = entityStats.attackLife;
        }

        Destroy(projectile, 2f); 
        Debug.Log("Projectile fired. Position: " + position + ", Velocity: " + velocity);
    }
}