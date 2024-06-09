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
        entityStats = GetComponent<PlayerMovement>();

        if (Mathf.Approximately(Input.GetAxis("Fire1"), 1) && Time.time > _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + attackSpeed;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-10f, 10f)) * Vector2.up; // Aleatoriza ligeiramente a direção
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
