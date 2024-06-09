using UnityEngine;

public class TankEnemy : Entity
{
    private GameObject _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag);
        if (_player is null)
            Debug.LogError("Nem um player foi encontrado");
    }

    protected override void Death()
    {
        base.Death();
        var player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Entity>();
        var score = GameObject.FindGameObjectWithTag(Constraints.HudTag).GetComponent<Hud>();

        score.scoreStats.AddScore(Data.PointsDroppedWhenDying);
        player.AddExp(Data.ExpDroppedWhenDying);
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
        if (!other.CompareTag(Constraints.PlayerTag)) return;

        other.GetComponent<Entity>().TakeDamage(Data.AttackDamage);
        Destroy(gameObject);
    }
}