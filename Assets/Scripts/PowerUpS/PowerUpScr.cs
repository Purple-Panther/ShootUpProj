using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp")]
public class PowerUpScr : ScriptableObject
{
    public string powerUpName;
    public string powerUpDescription;

    public Sprite powerUpSprite;
}
