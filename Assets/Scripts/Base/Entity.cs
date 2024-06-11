using System.Collections;
using Interfaces;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;
    private SpriteRenderer[] spriteRenderers;

    public EntityDataInstance Data { get; set; }

    public bool CanMove { get; set; } = true;

    protected virtual void Awake()
    {
        if (soData is not null)
            Data = new EntityDataInstance(soData);

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
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
            foreach (var spriteRenderer in spriteRenderers)
            {
                if (spriteRenderer.color != Color.red)
                {
                    StartCoroutine(HitBlink(spriteRenderer));
                }
            }
        }
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