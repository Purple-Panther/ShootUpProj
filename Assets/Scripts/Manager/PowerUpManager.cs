using UnityEngine;

namespace Manager
{
    public class PowerUpManager : MonoBehaviour
    {
        private Player _player;
        private PlayerShooting _playerShooting;

        private void Start()
        {
            _player = Constraints.PlayerGameObject.GetComponent<Player>();
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
