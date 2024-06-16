using UnityEngine;

public static class Constraints
{
    #region || Tags ||

    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string EnemyProjectileTag = "EnemyProjectile";
    public const string WallTag = "Wall";
    public const string BoundariesTag = "Boundaries";
    public const string HudTag = "Hud";

    #endregion

    #region || GameObject ||

    public static readonly GameObject PlayerGameObject = GameObject.FindGameObjectWithTag(PlayerTag);
    public static readonly GameObject[] EnemiesGameObjects= GameObject.FindGameObjectsWithTag(Constraints.EnemyTag);

    #endregion
}