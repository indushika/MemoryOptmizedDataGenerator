using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RuntimeGameData 
{
    private List<int> npcIds = default;
    private Dictionary<int, RuntimeData> runtimeNPCDataById;


    public List<int> NPCIds { get => npcIds;}
    public Dictionary<int, RuntimeData> RuntimeNPCDataById { get => runtimeNPCDataById; }

    public void AddNewNPCId(int npcId)
    {
        npcIds.Add(npcId);
    }
    public void AddNewNPC(int npcId, RuntimeData runtimeData)
    {
        runtimeNPCDataById.TryAdd(npcId, runtimeData); 
    }
}
