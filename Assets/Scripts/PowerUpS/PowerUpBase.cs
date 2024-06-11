using UnityEngine;

public abstract class PowerUpBase : ScriptableObject
{
    public string powerUpName;
    public string powerUpDescription;
    public Sprite powerUpSprite;

    public abstract void ApplyEffect(PlayerShooting playerShooting);
}