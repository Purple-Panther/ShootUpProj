using System.Collections;
using DefaultNamespace.PowerUpS;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaDamagePowerUp", menuName = "PowerUps/AreaDamage")]
public class AreaDamagePowerUp : PowerUpBase
{
    public float areaRadius = 3f;
    public float areaDamagePercentage = 0.5f;
    public GameObject areaIndicatorPrefab;

    private GameObject areaIndicatorInstance;

    public override PowerUpType Type => PowerUpType.AreaDamage;

    public override void ApplyEffect(PlayerShooting playerShooting)
    {
        playerShooting.OnProjectileHit += HandleProjectileHit;

        if (areaIndicatorPrefab is not null)
        {
            areaIndicatorInstance = Instantiate(areaIndicatorPrefab, Vector3.zero, Quaternion.identity);
            UpdateAreaIndicatorScale();
            areaIndicatorInstance.SetActive(false);
        }
    }

    private void HandleProjectileHit(Vector3 position, float damage)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, areaRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(Constraints.EnemyTag))
            {
                Entity enemy = hitCollider.GetComponent<Entity>();
                if (enemy is not null)
                {
                    enemy.TakeDamage(damage * areaDamagePercentage);
                }
            }
        }

        if (areaIndicatorInstance is not null)
        {
            areaIndicatorInstance.transform.position = position;
            UpdateAreaIndicatorScale();
            CoroutineManager.StartRoutine(ShowAreaIndicatorForSeconds(0.1f));
        }
    }

    private IEnumerator ShowAreaIndicatorForSeconds(float duration)
    {
        if (areaIndicatorInstance is not null)
        {
            areaIndicatorInstance.SetActive(true);
            yield return new WaitForSeconds(duration);
            areaIndicatorInstance.SetActive(false);
        }
    }

    private void UpdateAreaIndicatorScale()
    {
        if (areaIndicatorInstance is not null)
        {
            float scale = areaRadius * 2f;
            areaIndicatorInstance.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
