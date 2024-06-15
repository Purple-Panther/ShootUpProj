using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehavior : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5.0f;
    [SerializeField] private float orbitDistance = 2.0f;
    [SerializeField] private float orbitSpeed = 50.0f;

    private float _shootInterval;
    private float _attackDamage;
    private Rigidbody2D _rb;
    private Camera _mainCamera;
    private Transform _playerTransform;
    private float _angle;

    public void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    void Start()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure there is a main camera in the scene.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Entity playerEntity = player.GetComponent<Entity>();
            _shootInterval = playerEntity.Data.AttackSpeed * 1.2f;
            _attackDamage = playerEntity.Data.AttackDamage / 2f;
        }

        _rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(Shoot), 0f, _shootInterval);
    }

    void Update()
    {
        if (_playerTransform != null)
        {
            Orbit();
        }
    }

    void Orbit()
    {
        _angle += orbitSpeed * Time.deltaTime;
        float radians = _angle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * orbitDistance;
        transform.position = (Vector2)_playerTransform.position + offset;
    }

    void Shoot()
    {
        GameObject target = FindTarget();

        if (target != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 direction = (target.transform.position - transform.position).normalized;

            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
            if (rbProjectile != null)
                rbProjectile.velocity = direction * projectileSpeed;

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
                projectileScript.Initialize(_attackDamage);

            Debug.Log($"Guardian: Atirando em {target.name} com dano {_attackDamage}!");
        }
        else
        {
            Debug.Log("Guardian: Nenhum alvo encontrado para disparar.");
        }
    }

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 viewportPos = _mainCamera.WorldToViewportPoint(enemy.transform.position);

            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
