using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CloudMovementType
{
    SimpleExplosion, BigExplosion, Static, Random, Velocity
}

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> _cloudSprites;
    
    public void SpawnCloud(CloudMovementType cloudMovementType, Vector3 position, Vector3 velocity, float lifetime, float rotationSpeed = 0f, float scaleDifference = 0f, float alphaFade = 1f, Color color = default)
    {
        if (cloudMovementType == CloudMovementType.BigExplosion)
        {
            // Create a cloud each 15° around the position with 3 waves with different velocity
            for (var i = 0; i < 24; i++)
            {
                var angle = i * 15f;
                var direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
                CreateCloud(position, direction * 0.5f, lifetime, rotationSpeed, scaleDifference, alphaFade, color);
                CreateCloud(position, direction * 1f, lifetime, rotationSpeed, scaleDifference, alphaFade, color);
                CreateCloud(position, direction * 1.5f, lifetime, rotationSpeed, scaleDifference, alphaFade, color);
            }
        }
        else if (cloudMovementType == CloudMovementType.SimpleExplosion)
        {
            // Create a cloud each 15° around the position with random variation and 20 to 30 clouds in the center of the explosion
            for (var i = 0; i < 24; i++)
            {
                var angle = i * 15f;
                var direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
                CreateCloud(
                    position, 
                    direction * Random.Range(0.5f, 1.5f), 
                    lifetime * Random.Range(0.5f, 1.5f), 
                    rotationSpeed, 
                    scaleDifference * Random.Range(0.5f, 1.5f), 
                    alphaFade,
                    color,
                    "Above"
                );
            }
        }
        else if (cloudMovementType == CloudMovementType.Static)
        {
            CreateCloud(position, Vector3.zero, lifetime, rotationSpeed, scaleDifference, alphaFade, color);
        }
        else if (cloudMovementType == CloudMovementType.Random)
        {
            CreateCloud(position, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), lifetime, rotationSpeed, scaleDifference, alphaFade, color);
        }
        else if (cloudMovementType == CloudMovementType.Velocity)
        {
            CreateCloud(position, velocity, lifetime, rotationSpeed, scaleDifference, alphaFade, color);
        }
    }

    private void CreateCloud(Vector3 position, Vector3 velocity, float lifetime = 0.1f, float rotationSpeed = 0f, float scaleDifference = 0f, float alphaFade = 1f, Color color = default, string layer = "Default")
    {
        var imageDelayed = new GameObject("ImageDelayed");
        imageDelayed.transform.position = position;
        
        var afterImageSpriteRenderer = imageDelayed.AddComponent<SpriteRenderer>();
        afterImageSpriteRenderer.sprite = _cloudSprites[Random.Range(0, _cloudSprites.Count)];
        afterImageSpriteRenderer.sortingOrder = 0;
        afterImageSpriteRenderer.sortingLayerName = layer;
        
        if (color != default)
        {
            afterImageSpriteRenderer.color = color;
        }

        var rb = imageDelayed.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        
        var imgDelayed = imageDelayed.AddComponent<ImageDelayed>();
        imgDelayed.SetVelocity(velocity);
        imgDelayed.SetTimeToLive(lifetime);
        imgDelayed.SetRotationSpeed(rotationSpeed);
        imgDelayed.SetScaleDifference(scaleDifference);
        imgDelayed.SetAlphaFade(alphaFade);
    }
}
