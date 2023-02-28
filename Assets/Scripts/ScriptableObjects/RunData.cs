using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunData", menuName = "ScriptableObjects/RunData", order = 1)]
public class RunData : ScriptableObject
{
    public GameObject classType;
    
    public float GetSpeed()
    {
        return classType.GetComponent<ClassType>().Speed;
    }
}
