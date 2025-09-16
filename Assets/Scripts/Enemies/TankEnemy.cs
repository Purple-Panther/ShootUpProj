using Base;
using UnityEngine;
using Util;

namespace Enemies
{
    public class TankEnemy : Entity
    {
        private GameObject _player;

        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag);
            if (_player is null)
                UnityEngine.Debug.LogError("Nenhum player foi encontrado");
        }

        protected override void Death()
        {
            base.Death();
            var player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Entity>();
            var hud = Hud.GetOrFind();
            if (hud is null)
            {
                Debug.LogError("HUD not found in scene to add score.");
                return;
            }
            var score = hud.scoreStats;

            score.AddScore(Data.PointsDroppedWhenDying);
            player.AddExp(Data.ExpDroppedWhenDying);
        }

        private void Update()
        {
            if (_player is not null)
            {
                Vector3 direction = (_player.transform.position - transform.position).normalized;

                transform.position += direction * (Data.BaseSpeed * Time.deltaTime);
            }
            else
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Constraints.PlayerTag)) return;

            other.GetComponent<Entity>().TakeDamage(Data.AttackDamage);
            Destroy(gameObject);
        }
    }
}