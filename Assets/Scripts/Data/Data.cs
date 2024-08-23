using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct Data 
{
    private readonly int id;
    private readonly string name;

    private readonly Dictionary<StatType, int> baseValueByStat;
    private readonly List<AttributeType> attributeTypes;

    private RuntimeData runtimeData;

    public int ID { get => id; }
    public string Name { get => name;}
    public List<AttributeType> AttributesList => attributeTypes;

    public Dictionary<StatType, int> BaseValueByStat => baseValueByStat;

    public RuntimeData RuntimeData { get => runtimeData; }

    public Data(int npcID, string name, List<AttributeType> attributeTypes,
       Dictionary<StatType, int> baseValueByStat, RuntimeData runtimeData)
    {
        this.id = npcID;
        this.name = name;
        this.baseValueByStat = baseValueByStat; 
        this.attributeTypes = attributeTypes;
        this.runtimeData = runtimeData;
    }


    //Runtime Data -> to be used by other systems if necessary 

    //set runtime stat values if the NPC already existed (NPC Manager will handle setting) 
    //last known position, other runtime data needed; equipment, etc.

}
