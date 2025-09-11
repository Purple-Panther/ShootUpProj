using UnityEngine;
using Util;

public class DeadLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!gameObject.activeInHierarchy)
            return;
        
        if (other.gameObject.CompareTag(Constraints.EnemyTag))
        {
            Destroy(other.gameObject);
        }
    }
}