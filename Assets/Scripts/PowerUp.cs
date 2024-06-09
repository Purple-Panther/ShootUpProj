using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEnums.PowerUpType powerUpType;
    public float speed = 2f;

    private GameObject _player;
    private PlayerShooting _playerShooting;

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

        powerUpType = (PowerUpEnums.PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpEnums.PowerUpType)).Length);
    }

    void Update()
    {
        if (_player is null) return;
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
    }

    public void ActivatePowerUp(PowerUpEnums.PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpEnums.PowerUpType.MultiShot:
                _playerShooting.projectileCount += 2; // Adiciona mais 2 projétei
                break;
            case PowerUpEnums.PowerUpType.MachineGun:
                _playerShooting.attackSpeed -= 0.2f; // Diminui o tempo entre os tiros
                break;
        }
    }
}