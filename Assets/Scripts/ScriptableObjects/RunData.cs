using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponData
{
    public GameObject Weapon;
    public List<ThemeColor> WeaponLevels;
    public bool IsAwakened;
    
    public WeaponData(GameObject weapon, List<ThemeColor> weaponLevels, bool isAwakened)
    {
        Weapon = weapon;
        WeaponLevels = weaponLevels;
        IsAwakened = isAwakened;
    }
}

[CreateAssetMenu(fileName = "RunData", menuName = "ScriptableObjects/RunData", order = 1)]
public class RunData : ScriptableObject
{
    public GameObject ClassType;

    // Stats section
    public Stats FinalStats;
    // Stats that are up by collecting items, etc.
    public Stats AdditionalStats;
    private Stats _classTypeStats;
    private Stats _leftWeaponStats;
    private Stats _rightWeaponStats;

    public int CurrentDashCount { get; set; } = 0;
    
    // Weapons
    public WeaponData LeftWeapon { get; set; }
    public WeaponData RightWeapon { get; set; }
    
    public void OnSetupRoom(GameObject player)
    {
        var weaponsController = player.GetComponent<WeaponsController>();
        
        if (LeftWeapon != null)
        {
            var weaponObject = Instantiate(LeftWeapon.Weapon, player.transform);
            var weapon = weaponObject.GetComponent<Weapon>();
            
            weapon.ThemeColorsByLevel = LeftWeapon.WeaponLevels;
            weapon.IsAwakened = LeftWeapon.IsAwakened;
            weapon.SetupStats();
            
            weaponsController.SetLeftWeapon(weaponObject);
            
            _leftWeaponStats = weapon.OuterStats;
        }
        
        if (RightWeapon != null)
        {
            var weaponObject = Instantiate(RightWeapon.Weapon, player.transform);
            var weapon = weaponObject.GetComponent<Weapon>();

            weapon.ThemeColorsByLevel = RightWeapon.WeaponLevels;
            weapon.IsAwakened = RightWeapon.IsAwakened;
            weapon.SetupStats();
            
            weaponsController.SetRightWeapon(weaponObject);
            
            _rightWeaponStats = weapon.OuterStats;
        }

        FinalStats = new Stats();
        
        _classTypeStats = player.GetComponent<Stats>();

        CompileStats();
        
        CurrentDashCount = FinalStats._dashCharges;
    }

    public void CompileStats()
    {
        FinalStats.ResetStats();
        
        FinalStats.AddStats(_classTypeStats);
        FinalStats.AddStats(AdditionalStats);
        
        if (_leftWeaponStats != null)
        {
            FinalStats.AddStats(_leftWeaponStats);
        }
        
        if (_rightWeaponStats != null)
        {
            FinalStats.AddStats(_rightWeaponStats);
        }
    }
    
    public float GetSpeed()
    {
        return FinalStats.GetFinalSpeed();
    }
    
    public void SetClassType(GameObject classType)
    {
        LeftWeapon = null;
        RightWeapon = null;
        
        // Clone the classType
        ClassType = classType;
        
        // Set the starting weapon
        var startingWeapon = classType.GetComponent<ClassType>().StartingLeftWeapon;

        if (startingWeapon != null)
        {
            var weapon = startingWeapon.GetComponent<Weapon>();
            LeftWeapon = new WeaponData(startingWeapon.gameObject, weapon.ThemeColorsByLevel, weapon.IsAwakened);
        }

        startingWeapon = classType.GetComponent<ClassType>().StartingRightWeapon;
        
        if (startingWeapon != null)
        {
            var weapon = startingWeapon.GetComponent<Weapon>();
            RightWeapon = new WeaponData(startingWeapon.gameObject, weapon.ThemeColorsByLevel, weapon.IsAwakened);
        }
        
        AdditionalStats = new Stats();
    }
    
    public void SwapWeapons()
    {
        (LeftWeapon, RightWeapon) = (RightWeapon, LeftWeapon);
    }
}
