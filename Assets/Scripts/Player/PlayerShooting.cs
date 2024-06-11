using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Entity entityStats;
    public int projectileCount = 1; // Quantidade inicial de projéteis

    public float attackSpeed;
    private float _nextFireTime;

    private void Start()
    {
        entityStats = GetComponent<Entity>();
        attackSpeed = entityStats.Data.AttackSpeed;
    }

    void Update()
    {
        entityStats = GetComponent<Player>();

        if (Mathf.Approximately(Input.GetAxis("Fire1"), 1) && Time.time > _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + attackSpeed;
        }
    }

    void Shoot()
    {
        float spreadAngle = 45f; // Total spread angle
        float startAngle = -spreadAngle / 2; // Initial angle

        for (int i = 0; i < projectileCount; i++)
        {
            float angle;
            if (projectileCount > 1)
            {
                angle = startAngle + (spreadAngle / (projectileCount - 1)) * i; // Calculate the angle for this projectile
            }
            else
            {
                angle = 0; // If there's only one projectile, no need to spread
            }
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up; // Rotate the direction by the angle
            FireProjectile(transform.position, direction * projectileSpeed, Quaternion.identity);
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
