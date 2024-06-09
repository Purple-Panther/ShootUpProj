using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyRanged : Entity
{
    [FormerlySerializedAs("ProjectilePrefab")] [SerializeField]
    private GameObject projectilePrefab;  // prefab do projétil
    [FormerlySerializedAs("ProjectileSpeed")] [SerializeField]
    private float projectileSpeed = 5.0f; //velocidade do projétil

    private float _shootInterval;
    private Rigidbody2D _rb;

    private void Start()
    {
        _shootInterval = Data.AttackSpeed;
        _rb = GetComponent<Rigidbody2D>();

        Shoot();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, -Data.BaseSpeed);

        if (_shootInterval <= 0)
        {
            Shoot();
            _shootInterval = Data.AttackSpeed;
        }
        else
        {
            _shootInterval -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        if (rbProjectile is not null)
            rbProjectile.velocity = transform.up * (-1 * projectileSpeed);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript is null) return;

        projectileScript.Initialize(Data.AttackDamage);
        projectileScript.projectileLifeSpan = Data.AttackLife;
    }
}