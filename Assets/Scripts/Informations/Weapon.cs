using System;
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
    [SerializeField] private int _projectileNumber = 1;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileLifeTime;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private float _spreadAnglePerProjectile;
    
    [Header("Parameters")]
    [SerializeField] private bool _homing;
    [SerializeField] private bool _passThrough;

    public List<ThemeColor> ThemeColorsByLevel { get; set; }
    public bool IsAwakened { get; set; }
    
    // List of function that has subscribed to the events
    public List<Action> onWeaponShootSubscribers;
    public List<Action<Projectile>> onWeaponCreateProjectileSubscribers;

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
    
    public bool Homing => _homing;
    public bool PassThrough => _passThrough;
    
    //TODO: Manage firerate somewhere

    private void Start()
    {
        onWeaponShootSubscribers = new List<Action>();
        onWeaponCreateProjectileSubscribers = new List<Action<Projectile>>();
    }

    private void Shoot()
    {
        onWeaponShootSubscribers.ForEach(subscriber => subscriber.Invoke());
        
        for (int i = 0; i < _projectileNumber; i++)
        {
            //TODO: Complete all data here
            
            float angle = _spreadAngle + _spreadAnglePerProjectile * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            
            var projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetVelocity(direction * _projectileSpeed);
            
            onWeaponCreateProjectileSubscribers.ForEach(subscriber => subscriber.Invoke(projectile.GetComponent<Projectile>()));
        }
    }
}
