using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager 
{
    private Dictionary<int, Data> activeNPCById;
    private ReadOnlyGameData readOnlyGameData;
    private RuntimeGameData runtimeGameData;

    private IdProvider idProvider;
    private Generator generator;

    public Manager(ReadOnlyGameData readOnlyGameData, RuntimeGameData runtimeGameData)
    {
        this.readOnlyGameData = readOnlyGameData;

        this.runtimeGameData = runtimeGameData;

        idProvider = new IdProvider(runtimeGameData.NPCIds);

        generator = new Generator(readOnlyGameData);

        activeNPCById = new Dictionary<int, Data>();
    }

    public Dictionary<int, Data> ActiveNPCById { get => activeNPCById;}

    #region API
    public Data CreateNewNPC()
    {
        int npcId = idProvider.GetNPCId();

        var npc = generator.GenerateNPCData(npcId, null);

        ActiveNPCById.Add(npcId, npc);

        runtimeGameData.AddNewNPCId(npcId);
        runtimeGameData.AddNewNPC(npcId, npc.RuntimeData);

        return npc;
    }
    public Data? GetNPCDataById(int id)
    {
        if (ActiveNPCById == null) 
        {
            throw new NullReferenceException("NPCManager: GetNPCDataById: Active NPC By Id collection is not found");
        }

        if (ActiveNPCById.TryGetValue(id, out Data npc))
        {
            return npc;
        }

        return null;
    }

    public List<int> GetNPCIdsByStatType(StatType statType)
    {
        var npcIds = new List<int>();

        foreach (var item in activeNPCById)
        {
            var npcData = item.Value;

            if (npcData.BaseValueByStat.Keys.ToList().Contains(statType))
            {
                npcIds.Add(item.Key);
            }
        }

        return npcIds;
    }
    #endregion

    #region Implementation 
    private void LoadNPCData()
    {
        if (readOnlyGameData == null)
        {
            throw new NullReferenceException("NPCManager: LoadNPCData: Persistent Game Data not found.");
        }
        var runtimeNPCDataById = runtimeGameData.RuntimeNPCDataById;
        foreach (var data in runtimeNPCDataById)
        {
            var npc = generator.GenerateNPCData(data.Key, data.Value);

            activeNPCById.Add(data.Key, npc);
        }

    }
    #endregion

    #region Notes: Remove Later
    //initial - loads existing data and generate data using generator 

    //NPC creator 
    //generate unique NPC Ids everytime a new NPC is created
    //check against existing ones 
    //generate runtime

    //NPC generator; generates data 

    //fetch existing/ previously generated NPC data using NPC Ids (seed) 
    #endregion

}
