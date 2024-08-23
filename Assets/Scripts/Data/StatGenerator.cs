using System;
using System.Collections;
using System.Collections.Generic;


public class StatGenerator 
{
    private Dictionary<StatType, StatData> statsByType;
    private Dictionary<AttributeType, AttributeData> attributesByType;

    private Dictionary<StatType, int> baseValuesByStatType;

    public StatGenerator(Dictionary<StatType, StatData> statsByType, 
        Dictionary<AttributeType, AttributeData> attributesByType)
    {
        this.statsByType = statsByType;
        this.attributesByType = attributesByType;
    }


    #region API

    public Dictionary<StatType, int> GetBaseStats(List<AttributeType> attributes, int seedID)
    {
        
        baseValuesByStatType = GenerateBaseValuesByStatType(seedID);

        ApplyAttributesToBaseValues(attributes);

        return baseValuesByStatType;
    }
    #endregion

    #region Implementation
    private Dictionary<StatType, int> GenerateBaseValuesByStatType(int seedId)
    {
        var random = new Random(seedId);

        baseValuesByStatType = new Dictionary<StatType, int>();

        if (statsByType.Count <= 0)
        {
            throw new Exception("NPCStatGenerator: GetBaseStats: Stat Data not found.");
        }

        foreach (var stat in statsByType)
        {
            var statData = stat.Value;

            int baseValue = random.Next(statData.MinBaseValue, statData.MaxBaseValue);

            baseValuesByStatType.Add(stat.Key, baseValue);
        }

        return baseValuesByStatType;

    }

    private void ApplyAttributesToBaseValues(List<AttributeType> attributes)
    {
        if (attributesByType.Count <= 0)
        {
            //throw new Exception("NPCStatGenerator: ApplyAttributesToBaseValues: " +
            //    "Attribute Data missing from Persistent Data.");

            return;
        }

        if (baseValuesByStatType == null || baseValuesByStatType.Count <= 0)
        {
            throw new Exception("NPCStatGenerator: ApplyAttributesToBaseValues: Stat Base Values not initialized.");
        }

        var baseStatsToBeProcessed = baseValuesByStatType;

        foreach (var stat in baseStatsToBeProcessed)
        {
            foreach (var attribute in attributes)
            {
                if (attributesByType.TryGetValue(attribute, out AttributeData attributeData))
                {
                    var stats = attributeData.EffectAmountByStat;

                    if (stats.TryGetValue(stat.Key, out int value))
                    {
                        var multiplier = 1 + (value / 100f);

                        int processedValue = (int)(stat.Value * multiplier);

                        baseValuesByStatType[stat.Key] = processedValue;
                    }
                }
            }
        }
    }
    #endregion
}
