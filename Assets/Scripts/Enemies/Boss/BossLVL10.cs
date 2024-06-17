using UnityEngine;
using System.Collections;

public class BossLvl10 : Entity
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
    private float _meleeHitDamage;
    private bool _useBulletHell = true;
    private Transform _playerTransform;
    private bool _isAttacking;
    private bool _hasReachedTargetPosition;

    private Vector3 _targetPosition;
    private float _horizontalMovementDelay = 2.0f;
    private float _horizontalMovementTimer;

    private float BossHalfWidth => GetComponent<SpriteRenderer>().bounds.extents.x;
    private float _horizontalDirection;

    private Camera _mainCamera;

    protected override void Start()
    {
        base.Start();
        _mainCamera = Camera.main;
        _meleeHitDamage = Data.AttackDamage;
        _shootInterval = Data.AttackSpeed;

        _playerTransform = Constraints.PlayerGameObject.transform;

        if (_mainCamera is not null)
        {
            float screenTopEdge = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, _mainCamera.nearClipPlane)).y;
            _targetPosition = new Vector3(transform.position.x, screenTopEdge - (2 * _mainCamera.orthographicSize / 6), transform.position.z);
        }

        StartCoroutine(AttackRoutine());

        _horizontalDirection = Random.Range(-1f, 1f);
        _horizontalMovementTimer = _horizontalMovementDelay;
    }

    private void FixedUpdate()
    {
        if (!_hasReachedTargetPosition)
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
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.001f)
        {
            _hasReachedTargetPosition = true;
        }
    }

    private void RandomHorizontalMovement()
    {
        _horizontalMovementTimer -= Time.deltaTime;
        if (_horizontalMovementTimer <= 0)
        {
            _horizontalDirection = Random.Range(-1f, 1f);
            _horizontalMovementTimer = _horizontalMovementDelay;
        }

        Vector3 movement = new Vector3(_horizontalDirection * Data.BaseSpeed * Time.deltaTime, 0, 0);
        transform.position += movement;

        float screenWidth = _mainCamera.orthographicSize * Screen.width / Screen.height;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenWidth + BossHalfWidth, screenWidth - BossHalfWidth),
            transform.position.y,
            transform.position.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constraints.PlayerTag))
        {
            collision.GetComponent<Entity>().TakeDamage(_meleeHitDamage);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (_useBulletHell)
                yield return StartCoroutine(BulletHellAttack());
            else
                yield return StartCoroutine(SummonAttack());

            _useBulletHell = !_useBulletHell;
            yield return new WaitForSeconds(attackDuration);
        }
    }

    private IEnumerator BulletHellAttack()
    {
        _isAttacking = true;
        float endTime = Time.time + attackDuration;

        while (Time.time < endTime)
        {
            FireBulletHell();
            yield return new WaitForSeconds(_shootInterval);
        }

        _isAttacking = false;
    }

    private void FireBulletHell()
    {
        if (_playerTransform is null) return;

        Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
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
        _isAttacking = true;
        float endTime = Time.time + attackDuration;

        while (Time.time < endTime)
        {
            SummonEnemy();
            yield return new WaitForSeconds(3.0f);
        }

        _isAttacking = false;
    }

    private void SummonEnemy()
    {
        if (_mainCamera is null) return;

        float playerX = _playerTransform.position.x;

        float screenTopEdge = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, _mainCamera.nearClipPlane)).y;

        float randomXOffset = Random.Range(-spawnXOffsetRange, spawnXOffsetRange);
        float spawnX = playerX + randomXOffset;

        Vector3 spawnPosition = new Vector3(spawnX, screenTopEdge + 1f, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    protected override void Death()
    {
        base.Death();
        var player = Constraints.PlayerGameObject.GetComponent<Entity>();
        var score = GameObject.FindGameObjectWithTag(Constraints.HudTag).GetComponent<Hud>().scoreStats;

        score.AddScore(Data.PointsDroppedWhenDying);
        player.AddExp(Data.ExpDroppedWhenDying);
    }

}
