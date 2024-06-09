using Interfaces;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;

    public EntityDataInstance Data { get; set; }

    public bool CanMove { get; set; } = true;

    protected virtual void Awake()
    {
        if (soData is not null)
            Data = new EntityDataInstance(soData);
    }

    protected virtual void Death()
    {
        if (Data.Health > 0) return;

        Destroy(gameObject);

        // if (!gameObject.CompareTag(Constraints.PlayerTag))
        //     GameObject.FindGameObjectWithTag(Constraints.HudTag)
        //         .GetComponent<Hud>()
        //         .scoreStats
        //         .AddScore(Data.PointsWhenDying);
    }

    public void TakeDamage(float hpToRemove)
    {
        Data.Health -= hpToRemove;
        Death();
    }
}