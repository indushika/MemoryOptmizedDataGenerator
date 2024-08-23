using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttributeData 
{
    private int effectAmount;
    private AttributeType attributeType;
    private string attributeName;
    private Dictionary<StatType, int> effectAmountByStat;

    public int EffectAmount { get => effectAmount; set => effectAmount = value; }
    public AttributeType AttributeType { get => attributeType; set => attributeType = value; }
    public string AttributeName { get => attributeName; set => attributeName = value; }
    public Dictionary<StatType, int> EffectAmountByStat { get => effectAmountByStat; set => effectAmountByStat = value; }
}


