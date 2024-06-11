using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private EntityData soData;
    private SpriteRenderer[] _spriteRenderers;

    public EntityDataInstance Data { get; set; }

    public bool CanMove { get; set; } = true;

    protected virtual void Awake()
    {
        if (soData is not null)
            Data = new EntityDataInstance(soData);

        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float hpToRemove)
    {
        GameObject new_popup = Instantiate(Hud.Instance.damage_popup, this.gameObject.transform.position, Quaternion.identity );
        new_popup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), 5), ForceMode2D.Impulse);
        new_popup.GetComponentInChildren<Text>().text = hpToRemove.ToString();
        Destroy(new_popup, 1f);
        
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