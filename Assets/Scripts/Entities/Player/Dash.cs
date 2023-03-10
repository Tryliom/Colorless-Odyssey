using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [Header("Settings")]
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 2f;
    
    private PlayerInputController _playerInputController;
    private Rigidbody2D _rigidbody2D;
    private AfterImageGenerator _afterImageGenerator;
    private EffectManager _effectManager;
    
    private float _timer = 0f;
    private float _cooldownTimer = 0f;
    private float _maxCooldown = 0f;
    
    private int _maxDashCount = 1;

    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _afterImageGenerator = GetComponent<AfterImageGenerator>();
        _effectManager = GetComponent<EffectManager>();

        RefreshData();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshData();

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _rigidbody2D.velocity = _direction * _dashSpeed;
            
            if (_timer <= 0)
            {
                _afterImageGenerator.enabled = false;
            }
        }
        
        if (_runData.CurrentDashCount < _maxDashCount)
        {
            if (_cooldownTimer == 0)
            {
                _cooldownTimer = _maxCooldown;
            }
            
            _cooldownTimer -= Time.deltaTime;
            
            if (_cooldownTimer <= 0)
            {
                _runData.CurrentDashCount++;
                _cooldownTimer = 0;
            }
        }
        
        if (_playerInputController.dashValue && _runData.CurrentDashCount > 0 && _timer <= 0 && _playerInputController.moveValue != Vector2.zero)
        {
            _direction = _playerInputController.moveValue;
            _timer = _dashDuration;
            _runData.CurrentDashCount--;
            _afterImageGenerator.enabled = true;
        }

        _playerInputController.dashValue = false;
    }

    private void RefreshData()
    {
        _maxDashCount = _runData.FinalStats._dashCharges;
        _maxCooldown = _dashCooldown * (1f - _runData.FinalStats._dashCooldownReductionPercentage);

        if (_maxCooldown < 0.2f)
        {
            _maxCooldown = 0.2f;
        }
    }
}
