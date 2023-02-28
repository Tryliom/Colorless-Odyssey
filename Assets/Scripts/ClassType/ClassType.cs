using UnityEditor.Animations;
using UnityEngine;

public class ClassType : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _speed;
    
    public string Name => _name;
    public string Description => _description;
    public float Speed => _speed;
    
    
}