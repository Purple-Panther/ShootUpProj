using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyRanged : Entity
{
    [FormerlySerializedAs("ProjectilePrefab")] [SerializeField]
    private GameObject projectilePrefab; // prefab do projétil

    [FormerlySerializedAs("ProjectileSpeed")] [SerializeField]
    private float projectileSpeed = 5.0f; //velocidade do projétil

    private float _shootInterval;
    private Rigidbody2D _rb;

    protected override void Start()
    {
        base.Start();
        
        _shootInterval = Data.AttackSpeed;
        _rb = gameObject.GetComponent<Rigidbody2D>();

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

    protected override void Death()
    {
        base.Death();
        var player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Entity>();
        var score = GameObject.FindGameObjectWithTag(Constraints.HudTag).GetComponent<Hud>().scoreStats;

        score.AddScore(Data.PointsDroppedWhenDying);
        player.AddExp(Data.ExpDroppedWhenDying);
    }

    private void Shoot()
    {
        GameObject projectileGameObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        if (projectileGameObject is null) return;

        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        Rigidbody2D projectileRb = projectileGameObject.GetComponent<Rigidbody2D>();
        
        if (projectileRb is not null)
            projectileRb.velocity = transform.up * (-1 * projectileSpeed);

        projectile.Initialize(Data.AttackDamage);
        projectile.projectileLifeSpan = Data.AttackLife;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Constraints.PlayerTag)) return;

        other.GetComponent<Entity>().TakeDamage(Data.AttackDamage);
        Destroy(gameObject);
    }
}