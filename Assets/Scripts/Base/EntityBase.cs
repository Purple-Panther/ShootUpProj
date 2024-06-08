using UnityEngine;
using UnityEngine.Serialization;

public class EntityBase : MonoBehaviour
{
    [FormerlySerializedAs("SoData")] [SerializeField]
    private EntityData soData;

    public EntityDataInstance Data;

    private const string PlayerTag = "Player";
    private const string HudTag = "Hud";

    public virtual bool CanMove => true;

    protected virtual void Awake()
    {
        if(soData is not null)
            Data = new EntityDataInstance(soData);
    }

    protected virtual void Death()
    {
        if (Data.Health > 0) return;

        if (!gameObject.CompareTag(PlayerTag))
            GameObject.FindGameObjectWithTag(HudTag).GetComponent<ScoreStats>().AddScore(Data.PointsToDeath);

        Destroy(gameObject);
    }
}