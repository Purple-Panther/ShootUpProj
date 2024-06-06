using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { MultiShot, MachineGun }
    public PowerUpType powerUpType;
    public float powerUpDuration = 2f; // Duração do power-up em segundos
    public float speed = 2f; // Velocidade com que o power-up se move em direção ao jogador

    private Dictionary<PowerUpType, float> powerUpEndTimes = new Dictionary<PowerUpType, float>();
    private Dictionary<PowerUpType, bool> isPowerUpActive = new Dictionary<PowerUpType, bool>();
    private GameObject player;
    private PlayerShooting playerShooting;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponent<PlayerShooting>();
        powerUpType = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length); // Escolhe um tipo de power-up aleatório
        foreach (PowerUpType type in System.Enum.GetValues(typeof(PowerUpType)))
        {
            isPowerUpActive[type] = false;
        }
    }

    void Update()
    {
        // Move o power-up em direção ao jogador
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        // Verifica se o power-up está ativo e se o tempo atual é maior que o tempo de término do power-up
        if (isPowerUpActive[powerUpType] && Time.time > powerUpEndTimes[powerUpType])
        {
            DeactivatePowerUp(powerUpType);
        }
    }

    public void ActivatePowerUp()
    {
        if (!isPowerUpActive[powerUpType])
        {
            isPowerUpActive[powerUpType] = true;
            powerUpEndTimes[powerUpType] = Time.time + powerUpDuration;
            Debug.Log("PowerUp activated!");
    
            switch (powerUpType)
            {
                case PowerUpType.MultiShot:
                    playerShooting.shootMethod = PlayerShooting.ShootMethod.MultiShot;
                    break;
                case PowerUpType.MachineGun:
                    playerShooting.attackSpeed_ /= 3;
                    break;
            }
        }
    }

    public void DeactivatePowerUp(PowerUpType type)
    {
        isPowerUpActive[type] = false;
        Debug.Log("PowerUp deactivated!");

        switch (type)
        {
            case PowerUpType.MultiShot:
            case PowerUpType.MachineGun:
                playerShooting.shootMethod = PlayerShooting.ShootMethod.SingleShot;
                playerShooting.attackSpeed_ *= 2; // Reset attack speed
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ActivatePowerUp();
            Destroy(this.gameObject);
        }
    }
}