using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : MonoBehaviour
{
    public float chaseDistance = 5.0f; // Distance at which the entity starts chasing the player
    public float stopChaseDistance = 10.0f; // Distance at which the entity stops chasing the player
    public float speed = 2.0f; // Speed of the entity
    public int healAmount = 20; // Amount of health to heal on collision

    private Transform playerTransform;
    private bool isChasing = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= chaseDistance)
            {
                isChasing = true;
            }
            else if (distanceToPlayer > stopChaseDistance)
            {
                isChasing = false;
            }

            if (isChasing)
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Entity playerEntity = collision.GetComponent<Entity>();
            if (playerEntity != null)
            {
                playerEntity.Data.Health = Mathf.Min(playerEntity.Data.Health + healAmount, playerEntity.Data.MaxHealth);
                Destroy(gameObject);
            }
        }
    }
}