using UnityEngine;


public class EnemyMelee : MonoBehaviour
{
    float speed;
    public float zigzagTime = 2.0f;
    private float zigzagTimer;
    private int direction = 1;
    float meleeHitDamage;

    EntityStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EntityStats>();
        meleeHitDamage = enemyStats.attackDamage;
        speed = enemyStats.baseSpeed;
    }


    void Update()
    {
        // Move the enemy down the screen
        transform.position += new Vector3(direction * speed * Time.deltaTime, -speed * Time.deltaTime, 0);

        // Change direction every zigzagTime seconds
        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > zigzagTime)
        {
            direction *= -1;
            zigzagTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<EntityStats>().TakeDamage(meleeHitDamage);
            Destroy(gameObject);
        }
    }
}