using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp")]
public class PowerUpScr : ScriptableObject
{
    public string powerUpName; // "MultiShot" ou "MachineGun"
    public string powerUpDescription;
    public Sprite powerUpSprite;
}
