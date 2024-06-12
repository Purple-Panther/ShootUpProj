using UnityEngine;

public class EntityDataInstance
{
    public int MaxHealth { get; set; }
    public float Health { get; set; }
    public float BaseSpeed { get; set; }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackLife { get; set; }
    public float ExpDroppedWhenDying { get; set; }
    public int PointsDroppedWhenDying { get; set; }
    public int Level { get; set; }
    public float Exp { get; set; }
    public float ExpToNextLevel { get; set; }

    public float CurrentSpeed { get; set; }

    public bool CanEarnExp { get; set; } = true;
    public bool CanLevelUp => Exp >= ExpToNextLevel;

    public EntityDataInstance(EntityData entityData)
    {
        MaxHealth = entityData.maxHealth;
        Health = entityData.health;
        BaseSpeed = entityData.baseSpeed;
        AttackDamage = entityData.attackDamage;
        AttackSpeed = entityData.attackSpeed;
        AttackLife = entityData.attackLife;
        PointsDroppedWhenDying = entityData.pointsDroppedWhenDying;
        ExpDroppedWhenDying = entityData.expDroppedWhenDying;
        ExpToNextLevel = entityData.expToNextLevel;
        Exp = entityData.exp;
        Level = entityData.level;
    }

    public void LevelUp()
    {
        Level++;
        Exp = 0;
        ExpToNextLevel *= 1.5f; // Ajuste do multiplicador para balancear o progresso
    }
}