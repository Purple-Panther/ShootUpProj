using System;

public class EntityDataInstance
{
    public event Action OnDataChanged;

    private int _maxHealth;

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _healthHealth;

    public float Health
    {
        get => _healthHealth;
        set
        {
            _healthHealth = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _baseSpeed;

    public float BaseSpeed
    {
        get => _baseSpeed;
        set
        {
            _baseSpeed = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _attackDamage;

    public float AttackDamage
    {
        get => _attackDamage;
        set
        {
            _attackDamage = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _attackSpeed;

    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _attackLife;

    public float AttackLife
    {
        get => _attackLife;
        set
        {
            _attackLife = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _expDroppedWhenDying;

    public float ExpDroppedWhenDying
    {
        get => _expDroppedWhenDying;
        set
        {
            _expDroppedWhenDying = value;
            OnDataChanged?.Invoke();
        }
    }

    private int _pointsDroppedWhenDying;

    public int PointsDroppedWhenDying
    {
        get => _pointsDroppedWhenDying;
        set
        {
            _pointsDroppedWhenDying = value;
            OnDataChanged?.Invoke();
        }
    }

    private int _level;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _exp;

    public float Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _expToNextLevel;

    public float ExpToNextLevel
    {
        get => _expToNextLevel;
        set
        {
            _expToNextLevel = value;
            OnDataChanged?.Invoke();
        }
    }

    private float _currentSpeed;

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set
        {
            _currentSpeed = value;
            OnDataChanged?.Invoke();
        }
    }


    public bool CanEarnExp { get; set; } = true;
    public bool CanLevelUp => Exp >= ExpToNextLevel;


    public EntityDataInstance(EntityData entityData)
    {
        MaxHealth = entityData.maxHealth;
        Health = entityData.health;
        BaseSpeed = entityData.baseSpeed;
        AttackDamage = entityData.attackDamage;
        AttackSpeed = entityData.attackSpeed;
        AttackLife = entityData.attackLife;
        PointsDroppedWhenDying = entityData.pointsDroppedWhenDying;
        ExpDroppedWhenDying = entityData.expDroppedWhenDying;
        ExpToNextLevel = entityData.expToNextLevel;
        Exp = entityData.exp;
        Level = entityData.level;
    }

    public void LevelUp()
    {
        Level++;
        Exp = 0;
        ExpToNextLevel *= 1.5f; // Ajuste do multiplicador para balancear o progresso
    }
}