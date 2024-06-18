using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isPlayer;
    public float projectileLifeSpan;
    private float _projectileDamage;

    public delegate void ProjectileHitHandler(Vector3 position, float damage);
    public event ProjectileHitHandler OnProjectileHit;

    public void Initialize(float damage)
    {
        _projectileDamage = damage;
    }

    void Start()
    {
        Destroy(gameObject, projectileLifeSpan);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(Constraints.EnemyTag) && isPlayer) || 
            (collision.gameObject.CompareTag(Constraints.PlayerTag) && !isPlayer))
        {
            Entity entity = collision.gameObject.GetComponent<Entity>();
            if (entity is not null)
            {
                entity.TakeDamage(_projectileDamage);
                OnProjectileHit?.Invoke(transform.position, _projectileDamage);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No Entity component found on the collided gameObject.");
            }
        }
        else if (collision.gameObject.CompareTag(Constraints.BoundariesTag))
        {
            Destroy(gameObject);
        }
    }
}