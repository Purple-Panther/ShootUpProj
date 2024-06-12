using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{

    private Player _player;
    [SerializeField]
    private TMP_Text speed;
    [SerializeField]
    private TMP_Text currentSpeed;
    [SerializeField]
    private TMP_Text health;
    [SerializeField]
    private TMP_Text attackDamage;
    [SerializeField]
    private TMP_Text attackSpeed;
    [SerializeField]
    private TMP_Text exp;
    [SerializeField]
    private TMP_Text expToNextLevel;
    [SerializeField]
    private Toggle canDie;
    [SerializeField]
    private Toggle canEarnExp;
    [SerializeField]
    private Toggle hitKill;
    [SerializeField]
    private Toggle doNotDealDamage;
    [SerializeField]
    private Toggle maxAttackSpeed;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Constraints.PlayerTag).GetComponent<Player>();
    }

    private void Start()
    {
        #region || Text Fields ||

        currentSpeed.text = "Speed: 0";
        speed.text = "Max Speed: 0";
        health.text = "Health: 0";
        attackDamage.text = "Attack Damage: 0";
        attackSpeed.text = "Attack Speed: 0";
        exp.text = "Experience: 0";
        expToNextLevel.text = "ExpToLevelUp: 0";

        #endregion
    }

    private void Update()
    {
        currentSpeed.text = $"Speed: {_player.Data.CurrentSpeed}";
        speed.text = $"Max Speed: {_player.Data.BaseSpeed}";
        health.text = $"Health: {_player.Data.Health}";
        attackDamage.text = $"Attack Damage: {_player.Data.AttackDamage}";
        attackSpeed.text = $"Attack Speed: {_player.Data.AttackSpeed}";
        exp.text = $"Experience: {_player.Data.Exp}";
        expToNextLevel.text = $"ExpToLevelUp: {_player.Data.ExpToNextLevel}";
    }
}
