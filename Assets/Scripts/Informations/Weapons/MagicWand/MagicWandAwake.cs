using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWandAwake : MonoBehaviour
{
    private Weapon _weapon;
    
    private bool _isAwaken;
    
    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAwaken && _weapon.IsAwakened)
        {
            _isAwaken = true;
            _weapon.onWeaponCreateProjectileSubscribers.Add(OnProjectileCreated);
        }
    }
    
    private void OnProjectileCreated(Projectile projectile)
    {
        projectile.SetPassThroughWall(true);
    }
}
