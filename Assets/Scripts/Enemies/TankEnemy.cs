using UnityEngine;

public class TankEnemy : Entity
{
    private GameObject _player;

    private const string PlayerTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (_player is null)
            Debug.LogError("Nem um player foi encontrado");
    }

    private void Update()
    {
        if (_player is not null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            transform.position += direction * (Data.BaseSpeed * Time.deltaTime);
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(PlayerTag)) return;

        other.GetComponent<Entity>().TakeDamage(Data.AttackDamage);
        Destroy(gameObject);
    }
}