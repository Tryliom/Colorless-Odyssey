using UnityEditor.Animations;
using UnityEngine;

public class ClassType : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    
    public string Name => _name;
    public string Description => _description;
}