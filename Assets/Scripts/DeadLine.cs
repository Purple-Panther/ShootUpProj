using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(EnemyTag))
        {
            Destroy(other.gameObject);
        }
    }
}