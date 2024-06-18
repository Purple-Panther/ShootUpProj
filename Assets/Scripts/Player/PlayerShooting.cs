using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public EntityDataInstance EntityStats { get; private set; }
    public event Action<Vector3, float> OnProjectileHit;

    private Player _player;
    public float attackSpeed;
    private float _nextFireTime;
    private bool _isShootingInputDown = false;

    #region || Projectile ||
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    public int projectileCount = 1;
    private GameObject _projectileGameObject;
    private Projectile Projectile => _projectileGameObject.GetComponent<Projectile>();
    private Rigidbody2D ProjectileRb => _projectileGameObject.GetComponent<Rigidbody2D>();
    #endregion

    private void Start()
    {
        _player = Constraints.PlayerGameObject.GetComponent<Player>();
        EntityStats = _player.Data;
        attackSpeed = EntityStats.AttackSpeed;
    }

    private void Update()
    {
        _isShootingInputDown = Mathf.Approximately(Input.GetAxisRaw("Fire1"), 1);
    }

    private void FixedUpdate()
    {
        if (_isShootingInputDown && Time.time > _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + attackSpeed;
        }
    }

    private void Shoot()
    {
        const float spreadAngle = 45f;
        const float startAngle = -spreadAngle / 2;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = (projectileCount > 1) ? startAngle + (spreadAngle / (projectileCount - 1)) * i : 0;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
            FireProjectile(transform.position, direction * projectileSpeed, Quaternion.identity);
        }
    }

    private void FireProjectile(Vector2 position, Vector2 velocity, Quaternion rotation)
    {
        _projectileGameObject = Instantiate(projectilePrefab, position, rotation);
        ProjectileRb.velocity = velocity;

        if (Projectile is not null)
        {
            Projectile.Initialize(_player.Data.AttackDamage);
            Projectile.projectileLifeSpan = _player.Data.AttackLife;
            
            Projectile.OnProjectileHit += (pos, damage) => OnProjectileHit?.Invoke(pos, damage);
        }

        Destroy(_projectileGameObject, 2f);
    }
}