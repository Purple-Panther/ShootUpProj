using UnityEngine;


public class EnemyMelee : Entity
{
    [SerializeField]
    private float zigzagTime = 2.0f;

    private float _speed;
    private float _meleeHitDamage;
    private float _zigzagTimer;

    private int _direction = 1;


    private void Start()
    {
        _meleeHitDamage = Data.AttackDamage;
        _speed = Data.BaseSpeed;
    }


    private void Update()
    {
        // Move the enemy down the screen
        transform.position += new Vector3(_direction * _speed * Time.deltaTime, -_speed * Time.deltaTime, 0);

        // Change direction every zigzagTime seconds
        _zigzagTimer += Time.deltaTime;

        if (_zigzagTimer <= zigzagTime) return;

        _direction *= -1;
        _zigzagTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Constraints.PlayerTag)) return;

        TakeDamage(_meleeHitDamage);
        Destroy(gameObject);
    }
}