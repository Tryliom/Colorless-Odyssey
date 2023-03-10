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
    public Stats FinalStats;
    
    private Stats _classTypeStats;

    public int CurrentDashCount { get; set; } = 0;
    
    // Weapons
    public WeaponData LeftWeapon { get; set; }
    public WeaponData RightWeapon { get; set; }
    
    public void OnSetupRoom(GameObject player)
    {
        _classTypeStats = player.GetComponent<Stats>();
        FinalStats = new Stats();
        
        CompileStats();
        
        CurrentDashCount = FinalStats._dashCharges;
    }

    public void CompileStats()
    {
        FinalStats.ResetStats();
        
        FinalStats.AddStats(_classTypeStats);
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
    }
    
    public void SwapWeapons()
    {
        (LeftWeapon, RightWeapon) = (RightWeapon, LeftWeapon);
    }
}
