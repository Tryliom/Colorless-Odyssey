using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
{
    OneHanded, TwoHanded
}

public class Weapon : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [Header("Sprites per level")]
    [SerializeField] private List<SpriteRenderer> _spritesPerLevel;
    
    [Header("Projectile")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _projectileSpawnPoint;
    
    [Header("Informations")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _awakenedDescription;
    [SerializeField] private Rarity _rarity;
    [SerializeField] private WeaponType _weaponType;
    
    [Header("Stats")]
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _criticalChance;
    [SerializeField] private int _projectileNumber = 1;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileLifeTime;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private float _spreadAnglePerProjectile;
    
    [Header("Parameters")]
    [SerializeField] private bool _homing;
    [SerializeField] private bool _passThrough;
    [SerializeField] private bool _passThroughWall;
    [SerializeField] private bool _bouncing;
    
    [Header("Random shooting parameters")]
    [SerializeField] private float _radiusOffset;
    
    [Header("Animator")]
    [SerializeField] private float _animatorRadiusOffset;

    public List<ThemeColor> ThemeColorsByLevel { get; set; }
    public bool IsAwakened { get; set; }

    private Stats _innerStats;
    
    // List of function that has subscribed to the events
    public List<Action> onWeaponShootSubscribers;
    public List<Action<Projectile>> onWeaponCreateProjectileSubscribers;

    public string Name => _name;
    public string Description => _description;
    public string AwakenedDescription => _awakenedDescription;
    public Rarity Rarity => _rarity;
    public WeaponType WeaponType => _weaponType;
    
    public float MinDamage => _minDamage;
    public float MaxDamage => _maxDamage;
    public float AttackSpeed => _attackSpeed;
    public float CriticalChance => _criticalChance;
    public int ProjectileNumber => _projectileNumber;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileLifeTime => _projectileLifeTime;
    public float ProjectileKnockback => _projectileKnockback;
    public float SpreadAngle => _spreadAngle;
    public float SpreadAnglePerProjectile => _spreadAnglePerProjectile;
    
    public bool Homing => _homing;
    public bool PassThrough => _passThrough;
    public bool PassThroughWall => _passThroughWall;
    public bool Bouncing => _bouncing;
    
    public float AnimatorRadiusOffset { get => _animatorRadiusOffset; set => _animatorRadiusOffset = value; }
    
    public Stats OuterStats { get; private set; }
    
    private Animator _animator;
    
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetupStats()
    {
        ThemeColorsByLevel = new List<ThemeColor>();
        onWeaponShootSubscribers = new List<Action>();
        onWeaponCreateProjectileSubscribers = new List<Action<Projectile>>();

        OuterStats = new Stats();
        _innerStats = new Stats();
        
        UpdateStats();
    }

    public void Shoot(Vector2 targetPosition)
    {
        onWeaponShootSubscribers.ForEach(subscriber => subscriber.Invoke());
        
        _animator.SetTrigger(Shoot1);
        
        for (var i = 0; i < GetProjectileNumber(); i++)
        {
            var position = _projectileSpawnPoint.transform.position + new Vector3(Random.Range(-_radiusOffset, _radiusOffset), Random.Range(-_radiusOffset, _radiusOffset), 0);
            var direction = (targetPosition - (Vector2) position).normalized;
            var totalSpreadAngle = _spreadAngle + _spreadAnglePerProjectile * (GetProjectileNumber() - 1);
            direction = Quaternion.Euler(0, 0, Random.Range(- totalSpreadAngle / 2, totalSpreadAngle / 2)) * direction;
            
            var projectile = Instantiate(_projectilePrefab, position, Quaternion.identity);
            var projectileScript = projectile.GetComponent<Projectile>();
            var damage = GetDamage();
            var isCritical = GetCritical();
            
            if (isCritical)
            {
                damage *= 1 + _runData.FinalStats._criticalDamage;
            }
            
            // Set the projectile's stats
            projectileScript.SetVelocity(direction * _projectileSpeed);
            projectileScript.SetCritical(isCritical);
            projectileScript.SetDamage(damage);
            projectileScript.SetKnockback(GetKnockback());
            projectileScript.SetLifeTime(_projectileLifeTime);
            projectileScript.SetHoming(_homing);
            projectileScript.SetPassThrough(_passThrough);
            projectileScript.SetPassThroughWall(_passThroughWall);
            projectileScript.SetBouncing(_bouncing);
            projectileScript.SetTag("PlayerProjectile");
            
            onWeaponCreateProjectileSubscribers.ForEach(subscriber => subscriber.Invoke(projectileScript));
        }
    }

    public void LevelUp(ThemeColor themeColor)
    {
        ThemeColorsByLevel.Add(themeColor);
        
        UpdateStats();
    }
    
    public void FlipY(bool flipY)
    {
        foreach (var spriteRenderer in _spritesPerLevel)
        {
            spriteRenderer.flipY = flipY;
        }
    }

    private void UpdateStats()
    {
        _innerStats.ResetStats();
        OuterStats.ResetStats();
        
        // Attribute stats according to levels colors
        var themeColorCount = new Dictionary<ThemeColor, int>();
        var level = 0;
        
        foreach (var themeColor in ThemeColorsByLevel)
        {
            _spritesPerLevel[level].color = themeColor.Color;
            
            if (themeColorCount.ContainsKey(themeColor))
            {
                themeColorCount[themeColor]++;
            }
            else
            {
                themeColorCount.Add(themeColor, 1);
            }

            if (themeColor is YellowThemeColor)
            {
                if (themeColorCount[themeColor] == 1)
                {
                    _innerStats._attackSpeedPercentage += 0.1f;
                }
                else if (themeColorCount[themeColor] == 2)
                {
                    _innerStats._attackSpeedPercentage += 0.15f;
                }
                else if (themeColorCount[themeColor] == 3)
                {
                    
                }
                else if (themeColorCount[themeColor] == 4)
                {
                    OuterStats._thunderboltDamagePercentage += 0.3f;
                    OuterStats._thunderboltRadiusPercentage += 0.3f;
                }
                else if (themeColorCount[themeColor] == 5)
                {
                    
                }
            }
            
            level++;
        }
    }
    
    public float GetCooldown()
    {
        return 1f / _attackSpeed * (1 + _innerStats._attackSpeedPercentage + _runData.FinalStats._attackSpeedPercentage);
    }
    
    private float GetDamage()
    {
        return Random.Range(_minDamage, _maxDamage) * (1 + _innerStats._damagePercentage + _runData.FinalStats._damagePercentage);
    }
    
    private float GetKnockback()
    {
        return _projectileKnockback * (1 + _innerStats._projectileKnockbackPercentage + _runData.FinalStats._projectileKnockbackPercentage);
    }
    
    private bool GetCritical()
    {
        return Random.Range(0f, 1f) < _criticalChance + _innerStats._criticalChance + _runData.FinalStats._criticalChance;
    }
    
    private int GetProjectileNumber()
    {
        return _projectileNumber + _innerStats._projectileNumber + _runData.FinalStats._projectileNumber;
    }
}
