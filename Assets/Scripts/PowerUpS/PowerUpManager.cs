using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private PlayerShooting playerShooting;

    void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting component not found.");
        }
    }

    public void ActivatePowerUp(PowerUpBase powerUp)
    {
        powerUp.ApplyEffect(playerShooting);
    }
}