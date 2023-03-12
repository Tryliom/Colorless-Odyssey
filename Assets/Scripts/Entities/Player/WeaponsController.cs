using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [Header("Weapons position")]
    [SerializeField] private float _leftWeaponRadius = 0.3f;
    [SerializeField] private float _rightWeaponRadius = 0.4f;

    private GameObject _leftWeapon;
    private GameObject _rightWeapon;
    
    private PlayerInputController _playerInputController;
    private Weapon _leftWeaponScript;
    private Weapon _rightWeaponScript;
    
    private float _leftCooldownTimer = 0f;
    private float _rightCooldownTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanUseLeftWeapon())
        {
            var playerPosition = transform.position;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = mousePosition - playerPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var directionFromAngle = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

            // Set position according to the angle and radius not the direction
            _leftWeapon.transform.position = playerPosition + directionFromAngle * (_leftWeaponRadius + _leftWeaponScript.AnimatorRadiusOffset);
            _leftWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            // If oriented to the left, flip the sprite
            _leftWeaponScript.FlipY(angle is > 90 or < -90);

            if (CanUseRightWeapon())
            {
                _rightWeapon.transform.position = playerPosition + directionFromAngle * (_rightWeaponRadius + _rightWeaponScript.AnimatorRadiusOffset);
                _rightWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                // If oriented to the left, flip the sprite
                _rightWeaponScript.FlipY(angle is > 90 or < -90);
            }
        }
        
        if (_leftWeapon != null && _leftCooldownTimer > 0f)
        {
            _leftCooldownTimer -= Time.deltaTime;
        }
        
        if (_rightWeapon != null && _rightCooldownTimer > 0f)
        {
            _rightCooldownTimer -= Time.deltaTime;
        }
        
        if (_playerInputController.attackValue)
        {
            // There is a small time between use of the left and right weapon to make it feel more natural
            if (CanUseLeftWeapon() && _leftCooldownTimer <= 0f)
            {
                _leftWeaponScript.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _leftCooldownTimer = _leftWeaponScript.GetCooldown();
                
                // Summon cloud explosion effect
                _runData.CloudGenerator.SpawnCloud(CloudMovementType.SimpleExplosion, _leftWeapon.transform.position, Vector3.zero, 0.2f, 100f, -0.5f, 0.3f);
            } 
            else if (CanUseRightWeapon() && _rightCooldownTimer <= 0f)
            {
                _rightWeaponScript.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _rightCooldownTimer = _rightWeaponScript.GetCooldown();
                
                // Summon cloud explosion effect
                _runData.CloudGenerator.SpawnCloud(CloudMovementType.SimpleExplosion, _rightWeapon.transform.position, Vector3.zero, 0.2f, 100f, -0.5f, 0.3f);
            }   
        }
    }
    
    private bool CanUseLeftWeapon()
    {
        return _leftWeapon != null;
    }
    
    private bool CanUseRightWeapon()
    {
        return _rightWeapon != null && _leftWeaponScript.WeaponType != WeaponType.TwoHanded && _rightWeaponScript.WeaponType != WeaponType.TwoHanded;
    }
    
    public void SetLeftWeapon(GameObject weapon)
    {
        _leftWeapon = weapon;
        
        if (_leftWeapon != null)
        {
            _leftWeaponScript = _leftWeapon.GetComponent<Weapon>();
        }
    }
    
    public void SetRightWeapon(GameObject weapon)
    {
        _rightWeapon = weapon;
        
        if (_rightWeapon != null)
        {
            _rightWeaponScript = _rightWeapon.GetComponent<Weapon>();
        }
    }
    
    public void SwapWeapons()
    {
        (_leftWeapon, _rightWeapon) = (_rightWeapon, _leftWeapon);
    }

    public bool CanShoot()
    {
        return (CanUseLeftWeapon() && _leftCooldownTimer <= 0f) || (CanUseRightWeapon() && _rightCooldownTimer <= 0f);
    }

    public float GetWeaponCooldownPercentage()
    {
        float percentage;
        
        if (CanUseLeftWeapon() && CanUseRightWeapon())
        {
            if (_leftCooldownTimer < _rightCooldownTimer)
            {
                percentage = _leftCooldownTimer / _leftWeaponScript.GetCooldown();
            }
            else
            {
                percentage = _rightCooldownTimer / _rightWeaponScript.GetCooldown();
            }
        }
        else if (CanUseLeftWeapon())
        {
            percentage = _leftCooldownTimer / _leftWeaponScript.GetCooldown();
        }
        else if (CanUseRightWeapon())
        {
            percentage = _rightCooldownTimer / _rightWeaponScript.GetCooldown();
        }
        else
        {
            percentage = 0f;
        }

        return percentage;
    }
}
