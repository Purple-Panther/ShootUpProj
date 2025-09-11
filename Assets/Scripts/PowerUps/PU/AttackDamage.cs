using Player;
using UnityEngine;

namespace PowerUpS.PU
{
    [CreateAssetMenu(fileName = "AttackDamage", menuName = "PowerUps/AttackDamage")]
    public class AttackDamage : PowerUpBase
    {
        public float damageIncreasePercentage = 0.2f;

        public override PowerUpType Type => PowerUpType.AttackDamage;

        public override void ApplyEffect(PlayerShooting playerShooting)
        {
            playerShooting.EntityStats.AttackDamage += playerShooting.EntityStats.AttackDamage * damageIncreasePercentage;
        }
    }
}