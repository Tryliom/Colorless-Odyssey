using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
    
    [Header("Stats")]
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _criticalChance;
    [SerializeField] private int _projectileNumber;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileLifeTime;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private float _spreadAnglePerProjectile;

    public string Name => _name;
    public string Description => _description;
    public string AwakenedDescription => _awakenedDescription;
    public Rarity Rarity => _rarity;
    
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
    
}
