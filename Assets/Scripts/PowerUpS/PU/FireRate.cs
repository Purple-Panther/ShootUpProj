using UnityEngine;

[CreateAssetMenu(fileName = "FireRatePowerUp", menuName = "PowerUps/FireRate")]
public class FireRatePowerUp : PowerUpBase
{
    public float fireRateReduction;
    private const float MinAttackSpeed = 0.3f;

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        playerShooting.attackSpeed = Mathf.Max(playerShooting.attackSpeed - fireRateReduction, MinAttackSpeed);
    }
}
