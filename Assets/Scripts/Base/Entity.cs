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
        Destroy(gameObject);
    }

    public void TakeDamage(float hpToRemove)
    {
        Data.Health -= hpToRemove;
        if (Data.Health <= 0)
            Death();
    }

    public void AddExp(float xp)
    {
        Data.Exp += xp;
        if (Data.CanLevelUp)
            Data.LevelUp();
    }
}