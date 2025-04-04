using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace.PowerUpS;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;
    private SpriteRenderer[] _spriteRenderers;
    [SerializeField]
    private List<ItemDrop> dropList;

    public EntityDataInstance Data { get; set; }
    public bool CanMove { get; set; } = true;

    protected Dictionary<PowerUpType, (PowerUpBase powerUp, int quantity)> PowerUps;

    protected virtual void Awake()
    {
        if (soData is not null)
            Data = new EntityDataInstance(soData);

        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        PowerUps = new Dictionary<PowerUpType, (PowerUpBase powerUp, int quantity)>();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
        DropItems();
    }

    public void TakeDamage(float hpToRemove)
    {
        GameObject newPopup = Instantiate(Hud.Instance.damagePopup, this.gameObject.transform.position, Quaternion.identity );
        newPopup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 5), ForceMode2D.Impulse);
        newPopup.GetComponentInChildren<Text>().text = hpToRemove.ToString(CultureInfo.CurrentCulture);
        Destroy(newPopup, 1f);
        
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

    private void DropItems()
    {
        foreach (var itemDrop in dropList)
        {
            float dropRoll = Random.Range(0f, 1f);
            if (dropRoll <= itemDrop.dropChance)
            {
                Instantiate(itemDrop.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}