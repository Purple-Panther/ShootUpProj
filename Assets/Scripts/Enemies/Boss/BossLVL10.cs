using UnityEngine;
using System.Collections;

public class BossLVL10 : Entity
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Attack Settings")]
    [SerializeField] private Transform[] summonPoints;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float projectileSpeed; 
    [SerializeField] private float attackDuration;
    
    private float shootingInterval;
    private float meleeHitDamage;
    private bool useBulletHell = true;
    private Transform playerTransform;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        meleeHitDamage = Data.AttackDamage;
        shootingInterval = Data.AttackSpeed;

        StartCoroutine(AttackRoutine());
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
            yield return new WaitForSeconds(shootingInterval);
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
        int summonIndex = Random.Range(0, summonPoints.Length);
        Instantiate(enemyPrefab, summonPoints[summonIndex].position, Quaternion.identity);
    }
}
