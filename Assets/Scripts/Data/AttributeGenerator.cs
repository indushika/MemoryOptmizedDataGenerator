using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AttributeGenerator 
{
    Dictionary<AttributeType, AttributeData> attributesByType;

    public AttributeGenerator(Dictionary<AttributeType, AttributeData> attributesByType)
    {
        this.attributesByType = attributesByType;
    }

    #region API
    public List<AttributeType> GetAttributes(int minAttributeCount, int maxAttributeCount, int seedId)
    {
        var random = new Random(seedId);

        int attributeCount = random.Next(minAttributeCount, maxAttributeCount);

        if (attributesByType.Count < attributeCount)
        {
            throw new SystemException("NPCAttributeGenerator: GetAttributeBaseData: Attribute Data not found.");
        }

        var attributeTypesCollection = attributesByType.Keys.ToList();

        var attributes = new List<AttributeType>();

        for (int i = 0; i < attributeCount; i++)
        {
            attributes.Add(attributeTypesCollection[random.Next(0, attributeTypesCollection.Count)]);
        }

        return attributes;
    }
    #endregion
}
