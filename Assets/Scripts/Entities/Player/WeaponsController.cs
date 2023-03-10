using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    private GameObject _leftWeapon;
    private GameObject _rightWeapon;
    
    private PlayerInputController _playerInputController;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GetComponent<PlayerInputController>();

        /*_leftWeapon = _runData.LeftWeapon.Weapon;
        _rightWeapon = _runData.RightWeapon.Weapon;*/
        //TODO: Spawn weapons and assign them to _leftWeapon and _rightWeapon
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetLeftWeapon(GameObject weapon)
    {
        _leftWeapon = weapon;
    }
    
    public void SetRightWeapon(GameObject weapon)
    {
        _rightWeapon = weapon;
    }
    
    public void SwapWeapons()
    {
        (_leftWeapon, _rightWeapon) = (_rightWeapon, _leftWeapon);
    }
}
