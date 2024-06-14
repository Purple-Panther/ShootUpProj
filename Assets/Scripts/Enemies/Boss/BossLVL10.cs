using UnityEngine;
using System.Collections;

public class BossLVL10 : Entity
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Attack Settings")]
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float attackDuration;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnXOffsetRange = 2.0f;

    private float _shootInterval;
    private float meleeHitDamage;
    private bool useBulletHell = true;
    private Transform playerTransform;
    private bool isAttacking;
    private bool hasReachedTargetPosition;

    private Vector3 targetPosition;
    private float horizontalMovementDelay = 2.0f;
    private float horizontalMovementTimer;

    private float horizontalDirection;

    protected override void Start()
    {
        base.Start();
        meleeHitDamage = Data.AttackDamage;
        _shootInterval = Data.AttackSpeed;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float screenTopEdge = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, mainCamera.nearClipPlane)).y;
            targetPosition = new Vector3(transform.position.x, screenTopEdge - (2 * mainCamera.orthographicSize / 6), transform.position.z);
        }

        StartCoroutine(AttackRoutine());

        horizontalDirection = Random.Range(-1f, 1f);
        horizontalMovementTimer = horizontalMovementDelay;
    }

    private void FixedUpdate()
    {
        if (!hasReachedTargetPosition)
        {
            MoveToTargetPosition();
        }
        else
        {
            RandomHorizontalMovement();
        }
    }

    private void MoveToTargetPosition()
    {
        float step = Data.BaseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            hasReachedTargetPosition = true;
        }
    }

    private void RandomHorizontalMovement()
    {
        horizontalMovementTimer -= Time.deltaTime;
        if (horizontalMovementTimer <= 0)
        {
            horizontalDirection = Random.Range(-1f, 1f);
            horizontalMovementTimer = horizontalMovementDelay;
        }

        Vector3 movement = new Vector3(horizontalDirection * Data.BaseSpeed * Time.deltaTime, 0, 0);
        transform.position += movement;

        float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float bossHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenWidth + bossHalfWidth, screenWidth - bossHalfWidth),
            transform.position.y,
            transform.position.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constraints.PlayerTag))
        {
            collision.GetComponent<Entity>().TakeDamage(meleeHitDamage);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (useBulletHell)
                yield return StartCoroutine(BulletHellAttack());
            else
                yield return StartCoroutine(SummonAttack());

            useBulletHell = !useBulletHell;
            yield return new WaitForSeconds(attackDuration);
        }
    }

    private IEnumerator BulletHellAttack()
    {
        isAttacking = true;
        float endTime = Time.time + attackDuration;

        while (Time.time < endTime)
        {
            FireBulletHell();
            yield return new WaitForSeconds(_shootInterval);
        }

        isAttacking = false;
    }

    private void FireBulletHell()
    {
        if (playerTransform == null) return;

        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angleStep = spreadAngle / (numberOfProjectiles - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            Vector2 projectileDirection = RotateVector(directionToPlayer, currentAngle);

            GameObject tempProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            tempProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileSpeed;

            Projectile projectileScript = tempProjectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(Data.AttackDamage);
                projectileScript.projectileLifeSpan = Data.AttackLife;
            }
        }
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radian);
        float sin = Mathf.Sin(radian);

        float newX = vector.x * cos - vector.y * sin;
        float newY = vector.x * sin + vector.y * cos;

        return new Vector2(newX, newY);
    }

    private IEnumerator SummonAttack()
    {
        isAttacking = true;
        float endTime = Time.time + attackDuration;

        while (Time.time < endTime)
        {
            SummonEnemy();
            yield return new WaitForSeconds(3.0f);
        }

        isAttacking = false;
    }

    private void SummonEnemy()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        float playerX = playerTransform.position.x;

        float screenTopEdge = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, mainCamera.nearClipPlane)).y;

        float randomXOffset = Random.Range(-spawnXOffsetRange, spawnXOffsetRange);
        float spawnX = playerX + randomXOffset;

        Vector3 spawnPosition = new Vector3(spawnX, screenTopEdge + 1f, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    protected override void Death()
    {
        base.Death();
        var player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Entity>();
        var score = GameObject.FindGameObjectWithTag(Constraints.HudTag).GetComponent<Hud>().scoreStats;

        score.AddScore(Data.PointsDroppedWhenDying);
        player.AddExp(Data.ExpDroppedWhenDying);
    }

}
