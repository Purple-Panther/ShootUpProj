using Player;
using UnityEngine;

namespace PowerUpS.PU
{
    [CreateAssetMenu(fileName = "ExtraProjectile", menuName = "PowerUps/ExtraProjectile")]
    public class ExtraProjectile : PowerUpBase
    {
        public int additionalProjectiles;

        public override PowerUpType Type => PowerUpType.ExtraProjectile;

        public override void ApplyEffect(PlayerShooting playerShooting)
        {
            playerShooting.projectileCount += additionalProjectiles;
        }
    }
}