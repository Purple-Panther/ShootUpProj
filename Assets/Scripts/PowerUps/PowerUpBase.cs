using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace PowerUpS
{
    public abstract class PowerUpBase : ScriptableObject
    {
        [FormerlySerializedAs("Name")] public string puName;
        [FormerlySerializedAs("Description")] public string description;
        [FormerlySerializedAs("Sprite")] public Sprite sprite;

        public abstract PowerUpType Type { get; }

        public abstract void ApplyEffect(PlayerShooting playerShooting);
    }
}