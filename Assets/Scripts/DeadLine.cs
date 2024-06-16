using UnityEngine;

public class DeadLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Constraints.EnemyTag))
        {
            Destroy(other.gameObject);
        }
    }
}