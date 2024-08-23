using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReadOnlyGameData
{
    public IReadOnlyDictionary<int, List<string>> FirstNamesCollectionByLength;
    public IReadOnlyDictionary<int, List<string>> LastNamesCollectionByLength;

    public IReadOnlyDictionary<AttributeType, AttributeData> AttributesByType;
    public IReadOnlyDictionary<StatType,  StatData> StatsByType;

}
