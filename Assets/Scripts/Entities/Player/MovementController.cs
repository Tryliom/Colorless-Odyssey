using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [SerializeField] private float _cloudSpawnRate = 0.25f;
    [SerializeField] private float _cloudLifetime = 0.3f;
    [SerializeField] private float _cloudRotationSpeed = 100f;
    
    private PlayerInputController _playerInputController;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    
    private float _cloudTimer = 0f;

    private static readonly int Running = Animator.StringToHash("Running");

    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = _playerInputController.moveValue * _runData.GetSpeed();

        _animator.SetBool(Running, _playerInputController.moveValue != Vector2.zero);
        
        if (_animator.GetBool(Running))
        {
            _cloudTimer -= Time.deltaTime;
            
            if (_cloudTimer <= 0)
            {
                var footPosition = transform.position + new Vector3(0, -0.2f, 0);
                _runData.CloudGenerator.SpawnCloud(CloudMovementType.Random, footPosition, Vector3.zero, _cloudLifetime, _cloudRotationSpeed, -0.5f);
                _cloudTimer = _cloudSpawnRate;
            }
        }
    }
}
