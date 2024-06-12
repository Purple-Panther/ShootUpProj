using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class EliteRangedEnemy : Entity
{
    [FormerlySerializedAs("ProjectilePrefab")] [SerializeField]
    private GameObject projectilePrefab;
    [FormerlySerializedAs("ProjectileSpeed")] [SerializeField]
    private float projectileSpeed = 5.0f;

    public float stopYPositionLowerBound = 0f; // Limite inferior da posição Y onde o inimigo deve parar
    public float stopYPositionUpperBound = 5f; // Limite superior da posição Y onde o inimigo deve parar

    private float _shootInterval;
    private bool _isMoving = true;

    private Rigidbody2D _rb;

    protected override void Start()
    {
        base.Start();
        _shootInterval = Data.AttackSpeed;
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.velocity = new Vector2(0, -Data.BaseSpeed);
            if (transform.position.y <= stopYPositionUpperBound && transform.position.y >= stopYPositionLowerBound)
            {
                _rb.velocity = Vector2.zero;
                _isMoving = false;
            }
        }

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

    protected void Update()
    {
        transform.Rotate(0, 0, 22.5f * Time.deltaTime);
    }

    private void Shoot()
    {
        float[] angles = { 0, 60, 120, 180, 240, 300 };

        for (int i = 0; i < 6; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            float angle = angles[i] + transform.eulerAngles.z;
            projectile.transform.Rotate(0, 0, angle);

            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
            if (rbProjectile is not null)
                rbProjectile.velocity = projectile.transform.up * projectileSpeed;

            Projectile projectileScript = projectile.GetComponent<Projectile>();

            if (projectileScript is null) return;

            projectileScript.Initialize(Data.AttackDamage);
            projectileScript.projectileLifeSpan = Data.AttackLife;
        }
    }
}