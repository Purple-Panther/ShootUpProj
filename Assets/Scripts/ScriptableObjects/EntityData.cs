using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "EntityData")]
public class EntityData : ScriptableObject
{
    public int maxHealth;
    public float health;
    public float baseSpeed;
    public float attackDamage;
    public float attackSpeed;
    public float attackLife;
    public int pointsDroppedWhenDying;
    public float expDroppedWhenDying;
    public float exp;
    public float expToNextLevel;
    public int level = 1;
}