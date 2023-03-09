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

    // Make that we can addition two Stats instance
    public static Stats operator +(Stats a, Stats b)
    {
        var c = new Stats();
        
        c._redHearts = a._redHearts + b._redHearts;
        c._blueHearts = a._blueHearts + b._blueHearts;
        c._greenHearts = a._greenHearts + b._greenHearts;
        c._speed = a._speed + b._speed;
        c._criticalChance = a._criticalChance + b._criticalChance;
        c._criticalDamage = a._criticalDamage + b._criticalDamage;
        c._attackSpeedPercentage = a._attackSpeedPercentage + b._attackSpeedPercentage;
        c._damagePercentage = a._damagePercentage + b._damagePercentage;
        c._speedPercentage = a._speedPercentage + b._speedPercentage;
        c._projectileSpeedPercentage = a._projectileSpeedPercentage + b._projectileSpeedPercentage;
        c._projectileSizePercentage = a._projectileSizePercentage + b._projectileSizePercentage;
        c._projectileKnockbackPercentage = a._projectileKnockbackPercentage + b._projectileKnockbackPercentage;
        c._projectileNumber = a._projectileNumber + b._projectileNumber;
        c._homingShots = a._homingShots || b._homingShots;
        c._pierceShots = a._pierceShots || b._pierceShots;
        c._dashCharges = a._dashCharges + b._dashCharges;
        c._dashCooldownReductionPercentage = a._dashCooldownReductionPercentage + b._dashCooldownReductionPercentage;
        c._aoeDamagePercentage = a._aoeDamagePercentage + b._aoeDamagePercentage;
        c._aoeRadiusPercentage = a._aoeRadiusPercentage + b._aoeRadiusPercentage;
        c._thunderboltDamagePercentage = a._thunderboltDamagePercentage + b._thunderboltDamagePercentage;
        c._thunderboltRadiusPercentage = a._thunderboltRadiusPercentage + b._thunderboltRadiusPercentage;
        c._goldGainPercentage = a._goldGainPercentage + b._goldGainPercentage;
        
        return c;
    }
}
