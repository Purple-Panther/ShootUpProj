using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehavior : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5.0f;

    private float _shootInterval;
    private float _attackDamage;
    private Rigidbody2D _rb;
    private Camera _mainCamera;

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

            // Debug para informar o alvo e o dano do disparo
            Debug.Log($"Guardian: Atirando em {target.name} com dano {_attackDamage}!");
        }
        else
        {
            // Debug se nenhum alvo for encontrado dentro da viewport
            Debug.Log("Guardian: Nenhum alvo encontrado para disparar.");
        }
    }

    GameObject FindTarget()
    {
        // Encontra todos os inimigos na cena
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Itera sobre os inimigos encontrados para encontrar o mais próximo dentro da viewport
        foreach (GameObject enemy in enemies)
        {
            // Converte a posição do inimigo para viewport
            Vector3 viewportPos = _mainCamera.WorldToViewportPoint(enemy.transform.position);

            // Verifica se o inimigo está dentro da viewport (0 a 1 para X e Y)
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                // Calcula a distância do inimigo para o Guardian
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        // Retorna o inimigo mais próximo dentro da viewport
        return nearestEnemy;
    }
}
