using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isPlayer;
    public float projectileLifeSpan;
    private float projectileDamage; 

    public void Initialize(float damage)
    {
        projectileDamage = damage;
    }

    void Start()
    {
        Destroy(gameObject, projectileLifeSpan);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" && isPlayer == true) || (collision.gameObject.tag == "Player" && isPlayer == false))
        {
            collision.gameObject.GetComponent<EntityStats>().TakeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}