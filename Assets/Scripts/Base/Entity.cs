using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.PowerUpS;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private List<ItemDrop> dropList = new();

    private SpriteRenderer[] _spriteRenderers;

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
        GameObject newPopup = Instantiate(Hud.Instance.damagePopup, transform.position, Quaternion.identity);
        newPopup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 5), ForceMode2D.Impulse);
        newPopup.GetComponentInChildren<Text>().text = hpToRemove.ToString(CultureInfo.CurrentCulture);
        Destroy(newPopup, 1f);

        Data.Health -= hpToRemove;

        if (CompareTag("Player"))
        {
            SFXManager.Instance?.PlayPlayerDamage();
        }
        else
        {
            SFXManager.Instance?.PlayEnemyDamage();
        }

        if (Data.Health <= 0)
        {
            if (CompareTag("Player"))
            {
                SFXManager.Instance?.PlayPlayerDeath();
            }
            else
            {
                SFXManager.Instance?.PlayEnemyDeath();
            }

            float delay = SFXManager.Instance != null ? 0.2f : 0f;
            StartCoroutine(DeathAfterDelay(delay));
        }
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

    private IEnumerator DeathAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
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
        if (dropList == null || dropList.Count == 0) return;

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