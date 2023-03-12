using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private float _timeSinceLastImage = 0f;
    private readonly float _timeBetweenImages = 0.025f;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRenderer.sprite == null) return;
        
        _timeSinceLastImage += Time.deltaTime;
        
        if (_timeSinceLastImage >= _timeBetweenImages && _spriteRenderer.sprite != null)
        {
            _timeSinceLastImage = 0f;
            GenerateAfterImage();
        }
    }
    
    private void GenerateAfterImage()
    {
        var imageDelayed = new GameObject("ImageDelayed");
        imageDelayed.transform.position = transform.position;
        imageDelayed.transform.rotation = transform.rotation;
        imageDelayed.transform.localScale = transform.localScale;
        
        var afterImageSpriteRenderer = imageDelayed.AddComponent<SpriteRenderer>();
        afterImageSpriteRenderer.sprite = _spriteRenderer.sprite;
        afterImageSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
        afterImageSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
        afterImageSpriteRenderer.flipX = _spriteRenderer.flipX;
        
        var rb = imageDelayed.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        
        imageDelayed.AddComponent<ImageDelayed>();
    }
}
