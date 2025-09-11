using Base;
using UnityEngine;
using Util;

namespace Misc
{
    public class LifeDrop : MonoBehaviour
    {
        public float chaseDistance = 5.0f; 
        public float stopChaseDistance = 10.0f; 
        public float speed = 2.0f;
        public int healAmount = 20; 
        private Transform playerTransform;
        private bool isChasing = false;

        void Start()
        {
            playerTransform = Constraints.PlayerGameObject.transform;
        }

        void Update()
        {
            if (playerTransform is not null)
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
            transform.position += direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constraints.PlayerTag))
            {
                Entity playerEntity = collision.GetComponent<Entity>();
                if (playerEntity is not null)
                {
                    playerEntity.Data.Health = Mathf.Min(playerEntity.Data.Health + healAmount, playerEntity.Data.MaxHealth);
                    Destroy(gameObject);
                }
            }
        }
    }
}