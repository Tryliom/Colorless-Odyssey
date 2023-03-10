using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private float _timeToLive = 0.1f;
    private float _timeSinceSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0.8f - (0.8f * _timeSinceSpawn / _timeToLive));
        }
    }
}
