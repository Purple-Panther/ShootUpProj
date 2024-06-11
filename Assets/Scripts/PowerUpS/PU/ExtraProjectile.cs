using UnityEngine;

[CreateAssetMenu(fileName = "ExtraProjectile", menuName = "PowerUps/ExtraProjectile")]
public class ExtraProjectile : PowerUpBase
{
    public int additionalProjectiles;

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        playerShooting.projectileCount += additionalProjectiles;
    }
}