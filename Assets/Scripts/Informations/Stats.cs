using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Health, 1 = 1/2 heart")] 
    [SerializeField] public int _redHearts;
    [SerializeField] public int _blueHearts;
    [SerializeField] public int _greenHearts;

    [Header("Basic Stats")]
    [SerializeField] public float _speed;
    [SerializeField] public float _criticalChance;
    [SerializeField] public float _criticalDamage;

    [Header("Percentages")]
    [SerializeField] public float _attackSpeedPercentage;
    [SerializeField] public float _damagePercentage;
    [SerializeField] public float _speedPercentage;
    
    [Header("Projectile")]
    [SerializeField] public float _projectileSpeedPercentage;
    [SerializeField] public float _projectileSizePercentage;
    [SerializeField] public float _projectileKnockbackPercentage;
    [SerializeField] public int _projectileNumber;
    [SerializeField] public bool _homingShots;
    [SerializeField] public bool _pierceShots;
    
    [Header("Dash")]
    [SerializeField] public int _dashCharges;
    [SerializeField] public float _dashCooldownReductionPercentage;
    
    [Header("AoE")]
    [SerializeField] public float _aoeDamagePercentage;
    [SerializeField] public float _aoeRadiusPercentage;
    
    [Header("Thunderbolt")]
    [SerializeField] public float _thunderboltDamagePercentage;
    [SerializeField] public float _thunderboltRadiusPercentage;
    
    [Header("Other")]
    [SerializeField] public float _goldGainPercentage;


    public float GetFinalSpeed()
    {
        return _speed * (1 + _speedPercentage);
    }

    public void ResetStats()
    {
        _redHearts = 0;
        _blueHearts = 0;
        _greenHearts = 0;
        _speed = 0;
        _criticalChance = 0;
        _criticalDamage = 0;
        _attackSpeedPercentage = 0;
        _damagePercentage = 0;
        _speedPercentage = 0;
        _projectileSpeedPercentage = 0;
        _projectileSizePercentage = 0;
        _projectileKnockbackPercentage = 0;
        _projectileNumber = 0;
        _homingShots = false;
        _pierceShots = false;
        _dashCharges = 0;
        _dashCooldownReductionPercentage = 0;
        _aoeDamagePercentage = 0;
        _aoeRadiusPercentage = 0;
        _thunderboltDamagePercentage = 0;
        _thunderboltRadiusPercentage = 0;
        _goldGainPercentage = 0;
    }

    public void AddStats(Stats other)
    {
        _redHearts += other._redHearts;
        _blueHearts += other._blueHearts;
        _greenHearts += other._greenHearts;
        _speed += other._speed;
        _criticalChance += other._criticalChance;
        _criticalDamage += other._criticalDamage;
        _attackSpeedPercentage += other._attackSpeedPercentage;
        _damagePercentage += other._damagePercentage;
        _speedPercentage += other._speedPercentage;
        _projectileSpeedPercentage += other._projectileSpeedPercentage;
        _projectileSizePercentage += other._projectileSizePercentage;
        _projectileKnockbackPercentage += other._projectileKnockbackPercentage;
        _projectileNumber += other._projectileNumber;
        _homingShots = _homingShots || other._homingShots;
        _pierceShots = _pierceShots || other._pierceShots;
        _dashCharges += other._dashCharges;
        _dashCooldownReductionPercentage += other._dashCooldownReductionPercentage;
        _aoeDamagePercentage += other._aoeDamagePercentage;
        _aoeRadiusPercentage += other._aoeRadiusPercentage;
        _thunderboltDamagePercentage += other._thunderboltDamagePercentage;
        _thunderboltRadiusPercentage += other._thunderboltRadiusPercentage;
        _goldGainPercentage += other._goldGainPercentage;
    }
}
