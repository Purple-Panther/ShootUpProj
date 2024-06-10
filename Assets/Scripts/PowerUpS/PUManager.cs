using DefaultNamespace.PowerUpS;
using UnityEngine;

public class PUManager : MonoBehaviour
{
    public PowerUpType powerUpType;

    private GameObject _player;
    private PlayerShooting _playerShooting;

    // Define o valor mínimo para attackSpeed
    private const float MinAttackSpeed = 0.3f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerShooting = _player.GetComponent<PlayerShooting>();
        }
        else
        {
            Debug.LogError("Player not found.");
        }

        powerUpType = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
    }

    void Update()
    {
        if (_player is null) return;
    }

    public void ActivatePowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.ExtraProjectile:
                _playerShooting.projectileCount += 2; // Adiciona mais 2 projéteis
                break;
            case PowerUpType.FireRate:
                // Diminui o tempo entre os tiros, mas não permite que seja menor que MinAttackSpeed
                _playerShooting.attackSpeed = Mathf.Max(_playerShooting.attackSpeed - 0.1f, MinAttackSpeed);
                break;
        }
    }
}