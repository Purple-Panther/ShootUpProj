using UnityEngine;

[CreateAssetMenu(fileName = "AttackDamage", menuName = "PowerUps/AttackDamage")]
public class AttackDamage : PowerUpBase
{
    public float damageIncreasePercentage = 0.2f; // Aumento de 20% no dano de ataque

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        playerShooting.entityStats.Data.AttackDamage += playerShooting.entityStats.Data.AttackDamage * damageIncreasePercentage; // Aumenta o dano de ataque do jogador em 20%
    }
}