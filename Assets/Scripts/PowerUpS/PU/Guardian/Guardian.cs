using DefaultNamespace.PowerUpS;
using UnityEngine;

[CreateAssetMenu(fileName = "GuardianPowerUp", menuName = "PowerUps/Guardian")]
public class Guardian : PowerUpBase
{
    public GameObject guardianPrefab;

    public override PowerUpType Type => PowerUpType.Guardian;

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        GameObject player = playerShooting.gameObject;
        GameObject guardianInstance = Instantiate(guardianPrefab, player.transform.position, Quaternion.identity);
        guardianInstance.GetComponent<GuardianBehavior>().Initialize(player.transform);
    }
}