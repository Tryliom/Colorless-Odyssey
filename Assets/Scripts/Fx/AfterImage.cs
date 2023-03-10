using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private float _timeToLive = 0.1f;
    private float _augmentScale = 0.3f;
    private float _timeSinceSpawn = 0f;
    
    private Vector2 _originalScale;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        
        if (_timeSinceSpawn >= _timeToLive)
        {
            Destroy(gameObject);
        }
        else
        {
            var color = _spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 0.8f - (0.8f * _timeSinceSpawn / _timeToLive));
            _spriteRenderer.color = color;
            
            transform.localScale = _originalScale + Vector2.one * (_augmentScale * _timeSinceSpawn) / _timeToLive;
        }
    }
}
