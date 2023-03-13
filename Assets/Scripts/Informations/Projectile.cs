using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    [SerializeField] private bool _spawnClouds;
    [SerializeField] private Color _cloudColor;

    private const float CloudSpawnRate = 0.1f;
    private float _cloudTimer;
    
    private float _damage;
    private float _lifetime;
    private float _knockback;
    
    private bool _isHoming;
    private bool _isCritical;
    private bool _passThrough;
    private bool _isBouncing;
    private bool _passThroughWall;
    
    private Vector2 _velocity;
    private float _timeAlive;
    
    private Rigidbody2D _rigidbody2D;
    private PolygonCollider2D _polygonCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = _velocity;
        
        // Rotate projectile to face direction of travel
        transform.up = _velocity;

        _timeAlive += Time.deltaTime;

        if (_timeAlive >= _lifetime)
        {
            DestroyProjectile();
        }
        
        if (_spawnClouds)
        {
            _cloudTimer -= Time.deltaTime;
            
            if (_cloudTimer <= 0)
            {
                _runData.CloudGenerator.SpawnCloud(CloudMovementType.Random, 
                    transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0),
                    Vector3.zero, 0.3f, 100f, 0.5f, 0.5f, _cloudColor);
                _cloudTimer = CloudSpawnRate;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("PlayerProjectile"))
        {
            //other.gameObject.GetComponent<EnemyState>().TakeDamage(_damage);
            
            if (!_passThrough)
            {
                DestroyProjectile();
            }
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("EnemyProjectile"))
        {
            //other.gameObject.GetComponent<PlayerState>().TakeDamage(_damage);
            
            if (!_passThrough)
            {
                DestroyProjectile();
            }
        }
        
        if (other.gameObject.CompareTag("Wall") && !_passThroughWall)
        {
            if (_isBouncing)
            {
                //TODO: Make it later
                _velocity = Vector2.Reflect(_velocity, other.transform.up);
            }
            else
            {
                DestroyProjectile();
            }
        }
    }

    public void DestroyProjectile()
    {
        _runData.CloudGenerator.SpawnCloud(CloudMovementType.SimpleExplosion, transform.position, Vector3.zero, 0.2f, 100f, -0.5f, 0.3f, _cloudColor);
            
        Destroy(gameObject);
    }
    
    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }
    
    public void SetDamage(float damage)
    {
        _damage = damage;   
    }

    public void SetLifeTime(float lifetime)
    {
        _lifetime = lifetime;
    }
    
    public void SetHoming(bool isHoming)
    {
        _isHoming = isHoming;
    }
    
    public void SetCritical(bool isCritical)
    {
        _isCritical = isCritical;
    }
    
    public void SetKnockback(float knockback)
    {
        _knockback = knockback;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
    
    public void SetPassThrough(bool passThrough)
    {
        _passThrough = passThrough;
    }
    
    public void SetBouncing(bool isBouncing)
    {
        _isBouncing = isBouncing;
    }
    
    public void SetPassThroughWall(bool passThroughWall)
    {
        _passThroughWall = passThroughWall;
    }
}
