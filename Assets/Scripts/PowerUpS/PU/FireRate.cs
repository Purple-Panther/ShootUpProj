using DefaultNamespace.PowerUpS;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRate", menuName = "PowerUps/FireRate")]
public class FireRatePowerUp : PowerUpBase
{
    public float fireRateReduction;
    private const float MinAttackSpeed = 0.3f;

    public override PowerUpType Type => PowerUpType.FireRate;

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        playerShooting.attackSpeed = Mathf.Max(playerShooting.attackSpeed - fireRateReduction, MinAttackSpeed);
    }
}
