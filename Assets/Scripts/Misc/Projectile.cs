using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isPlayer;
    public float projectileLifeSpan;
    private float _projectileDamage;

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
        if ((collision.gameObject.CompareTag(Constraints.EnemyTag) && isPlayer) || (collision.gameObject.CompareTag(Constraints.PlayerTag) && isPlayer == false))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(_projectileDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(Constraints.BoundariesTag))
        {
            Destroy(gameObject);
        }
    }
}