using UnityEngine;

public class EntityDataInstance
{
    public int MaxHealth { get; set; }
    public float Health { get; set; }
    public float BaseSpeed { get; set; }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackLife { get; set; }
    public int PointsToDeath { get; set; }
    public bool CanEntityAttack => Time.time > AttackSpeed;

    public EntityDataInstance(EntityData entityData)
    {
        MaxHealth = entityData.maxHealth;
        AttackDamage = entityData.attackDamage;
        Health = entityData.maxHealth;
        AttackSpeed = entityData.attackSpeed;
        BaseSpeed = entityData.baseSpeed;
        AttackDamage = entityData.attackDamage;
        AttackLife = entityData.attackLife;
        PointsToDeath = entityData.pointsToDeath;
    }
}