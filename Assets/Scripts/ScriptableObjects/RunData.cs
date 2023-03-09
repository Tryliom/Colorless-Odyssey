using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunData", menuName = "ScriptableObjects/RunData", order = 1)]
public class RunData : ScriptableObject
{
    public GameObject ClassType;
    public Stats FinalStats;
    
    private Stats _classTypeStats;

    public void OnSetupRoom(GameObject player)
    {
        _classTypeStats = player.GetComponent<Stats>();
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
}
