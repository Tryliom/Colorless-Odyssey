using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunData", menuName = "ScriptableObjects/RunData", order = 1)]
public class RunData : ScriptableObject
{
    public GameObject ClassType;
    public Stats FinalStats;
    
    public void CompileStats()
    {
        FinalStats = new Stats() + ClassType.GetComponent<Stats>();
    }
    
    public float GetSpeed()
    {
        return FinalStats.GetFinalSpeed();
    }
}
