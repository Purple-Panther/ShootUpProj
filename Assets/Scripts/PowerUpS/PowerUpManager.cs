using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private PlayerShooting _playerShooting;

    void Start()
    {
        _playerShooting = GetComponent<PlayerShooting>();
        if (_playerShooting is null)
        {
            Debug.LogError("PlayerShooting component not found.");
        }
    }

    public void ActivatePowerUp(PowerUpBase powerUp)
    {
        powerUp.ApplyEffect(_playerShooting);
    }
}