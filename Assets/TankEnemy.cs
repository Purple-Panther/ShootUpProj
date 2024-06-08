using System;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    private GameObject _player;
    private EntityStats _entityStats;

    private const string PlayerTag = "Player";

    void Awake()
    {
        _entityStats = GetComponent<EntityStats>();
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (_player is null)
            Debug.LogError("Nem um player foi encontrado");
    }

    void Update()
    {
        if (_player is not null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            transform.position += direction * (_entityStats.baseSpeed * Time.deltaTime);
        }
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            other.GetComponent<EntityStats>().TakeDamage(_entityStats.attackDamage);
            Destroy(gameObject);
        }
    }
}