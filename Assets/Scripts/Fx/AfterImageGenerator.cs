using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private float _timeSinceLastImage = 0f;
    private float _timeBetweenImages = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastImage += Time.deltaTime;
        
        if (_timeSinceLastImage >= _timeBetweenImages && _spriteRenderer.sprite != null)
        {
            _timeSinceLastImage = 0f;
            GenerateAfterImage();
        }
    }
    
    private void GenerateAfterImage()
    {
        var afterImage = new GameObject("AfterImage");
        afterImage.transform.position = transform.position;
        afterImage.transform.rotation = transform.rotation;
        afterImage.transform.localScale = transform.localScale;
        
        var afterImageSpriteRenderer = afterImage.AddComponent<SpriteRenderer>();
        afterImageSpriteRenderer.sprite = _spriteRenderer.sprite;
        afterImageSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
        afterImageSpriteRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;
        afterImageSpriteRenderer.flipX = _spriteRenderer.flipX;
        
        afterImage.AddComponent<AfterImage>();
    }
}
