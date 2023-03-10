using UnityEditor.Animations;
using UnityEngine;

public class ClassType : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _startingLeftWeapon;
    [SerializeField] private GameObject _startingRightWeapon;
    
    public string Name => _name;
    public string Description => _description;
    public GameObject StartingLeftWeapon => _startingLeftWeapon;
    public GameObject StartingRightWeapon => _startingRightWeapon;
}