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
    
    [Header("Percentages")]
    [SerializeField] public float _attackSpeedPercentage;
    [SerializeField] public float _damagePercentage;
    [SerializeField] public float _speedPercentage;
    [SerializeField] public float _projectileSpeedPercentage;
    [SerializeField] public float _projectileSizePercentage;
    [SerializeField] public float _projectileKnockbackPercentage;
    
    [Header("Other")]
    [SerializeField] public int _dashCharges;
    [SerializeField] public float _dashCooldownReductionPercentage;
    [SerializeField] public float _goldGainPercentage;
    [SerializeField] public bool _homingShots;
    [SerializeField] public bool _pierceShots;
    
    public float GetFinalSpeed()
    {
        return _speed * (1 + _speedPercentage);
    }

    // Make that we can addition two Stats instance
    public static Stats operator +(Stats a, Stats b)
    {
        var stats = new Stats();
        
        stats._redHearts = a._redHearts + b._redHearts;
        stats._blueHearts = a._blueHearts + b._blueHearts;
        stats._greenHearts = a._greenHearts + b._greenHearts;
        
        stats._speed = a._speed + b._speed;
        
        stats._attackSpeedPercentage = a._attackSpeedPercentage + b._attackSpeedPercentage;
        stats._damagePercentage = a._damagePercentage + b._damagePercentage;
        stats._speedPercentage = a._speedPercentage + b._speedPercentage;
        stats._projectileSpeedPercentage = a._projectileSpeedPercentage + b._projectileSpeedPercentage;
        stats._projectileSizePercentage = a._projectileSizePercentage + b._projectileSizePercentage;
        stats._projectileKnockbackPercentage = a._projectileKnockbackPercentage + b._projectileKnockbackPercentage;
        
        stats._dashCharges = a._dashCharges + b._dashCharges;
        stats._dashCooldownReductionPercentage = a._dashCooldownReductionPercentage + b._dashCooldownReductionPercentage;
        stats._goldGainPercentage = a._goldGainPercentage + b._goldGainPercentage;
        stats._homingShots = a._homingShots || b._homingShots;
        stats._pierceShots = a._pierceShots || b._pierceShots;
        
        return stats;
    }
}
