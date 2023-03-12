using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDelayed : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _velocity = Vector2.zero;
    private float _timeToLive = 0.1f;
    private float _rotationSpeed = 0f;
    private float _scaleDifference = 0.3f;
    private float _timeSinceSpawn = 0f;
    private float _alphaFade = 1f;
    
    private Vector2 _originalScale;

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = _velocity;
        _timeSinceSpawn += Time.deltaTime;
        
        if (_timeSinceSpawn >= _timeToLive)
        {
            Destroy(gameObject);
        }
        else
        {
            var color = _spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 1f - (_alphaFade * _timeSinceSpawn / _timeToLive));
            _spriteRenderer.color = color;
            
            transform.localScale = _originalScale + Vector2.one * (_scaleDifference * _timeSinceSpawn) / _timeToLive;
            transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
        }
    }
    
    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
    
    public void SetTimeToLive(float timeToLive)
    {
        _timeToLive = timeToLive;
    }
    
    public void SetRotationSpeed(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }
    
    public void SetScaleDifference(float scaleDifference)
    {
        _scaleDifference = scaleDifference;
    }
    
    public void SetAlphaFade(float alphaFade)
    {
        _alphaFade = alphaFade;
    }
}
