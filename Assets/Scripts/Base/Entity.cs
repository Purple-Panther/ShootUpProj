using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.PowerUpS;
using Interfaces;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;
    private SpriteRenderer[] _spriteRenderers;
    protected Dictionary<PowerUpType, (PowerUpBase powerUp, int quantity)> PowerUps;

    public EntityDataInstance Data { get; set; }

    public bool CanMove { get; set; } = true;

    protected virtual void Awake()
    {
        if (soData is not null)
            Data = new EntityDataInstance(soData);

        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        PowerUps = new Dictionary<PowerUpType, (PowerUpBase powerUp, int quantity)>();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float hpToRemove)
    {
        Data.Health -= hpToRemove;
        if (Data.Health <= 0)
            Death();
        else
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                if (spriteRenderer.color != Color.red)
                {
                    StartCoroutine(HitBlink(spriteRenderer));
                }
            }
        }
    }

    public void AddPowerUp(PowerUpBase powerUp)
    {
        var alreadyInPool = PowerUps.ContainsKey(powerUp.Type);

        if (alreadyInPool)
        {
            var powerUpTuple = PowerUps[powerUp.Type];
            var newQuantity = powerUpTuple.quantity + 1;
            PowerUps[powerUp.Type] = (powerUpTuple.powerUp, newQuantity);
            return;
        }

        PowerUps.Add(powerUp.Type, (powerUp, 1));
    }

    public void RemovePowerUp(PowerUpType powerUp)
    {
        var exitsInPool = PowerUps.ContainsKey(powerUp);

        if (exitsInPool)
            PowerUps.Remove(powerUp);
    }

    private IEnumerator HitBlink(SpriteRenderer spriteRenderer)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    public void AddExp(float xp)
    {
        Data.Exp += xp;
        if (Data.CanLevelUp)
            Data.LevelUp();
    }
}