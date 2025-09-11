using Player;
using PowerUpS;
using UnityEngine;
using Util;

namespace Manager
{
    public class PowerUpManager : MonoBehaviour
    {
        private Player.Player _player;
        private PlayerShooting _playerShooting;

        private void Start()
        {
            _player = Constraints.PlayerGameObject.GetComponent<Player.Player>();
            _playerShooting = _player.GetComponent<PlayerShooting>();

            if (_playerShooting is null)
            {
                Debug.LogError("PlayerShooting component not found.");
            }
        }

        public void ActivatePowerUp(PowerUpBase powerUp)
        {
            powerUp.ApplyEffect(_playerShooting);
        }
    }
}
