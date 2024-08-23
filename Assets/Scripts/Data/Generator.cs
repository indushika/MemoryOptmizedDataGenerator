using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator
{
    private ReadOnlyGameData persistentData;

    #region Generators
    private NameGenerator nameGenerator;
    private AttributeGenerator attributeGenerator;
    private StatGenerator statGenerator;
    #endregion

    #region Constants
    //later it should come from a GameSettings object
    private const int minCharacterLengthPerName = 3;
    private const int maxCharacterLengthPerName = 5;
    private const int minAttributeCount = 0;
    private const int maxAttributeCount = 2;
    #endregion


    public Generator(ReadOnlyGameData persistentData)
    {
        this.persistentData = persistentData;

        InitializeGenerators();
    }

    #region API

    public Data GenerateNPCData(int npcID, RuntimeData runtimeData)
    {
        var name = nameGenerator.GetName(maxCharacterLengthPerName, minCharacterLengthPerName, npcID);

        var attributes = attributeGenerator.GetAttributes(minAttributeCount, maxAttributeCount, npcID);

        var stats = statGenerator.GetBaseStats(attributes, npcID);

        if (runtimeData == null) runtimeData = new RuntimeData();
        
        Data npcData = new Data(npcID, name, attributes, stats, runtimeData);

        return npcData;
    }

    #endregion

    #region Implementation

    #region Initializers

    private void InitializeGenerators()
    {
        InitializeNameGenerator();

        InitializeAttributeGenerator();

        InitializeStatGenerator();
    }

    private void InitializeNameGenerator()
    {
        var firstNamesByCharacterCount = (Dictionary<int, List<string>>)persistentData.FirstNamesCollectionByLength;

        var lastNamesByCharacterCount = (Dictionary<int, List<string>>)persistentData.LastNamesCollectionByLength;

        nameGenerator = new NameGenerator(firstNamesByCharacterCount, lastNamesByCharacterCount);
    }

    private void InitializeAttributeGenerator()
    {
        var attributesByType = (Dictionary<AttributeType, AttributeData>)persistentData.AttributesByType;

        attributeGenerator = new AttributeGenerator(attributesByType);
    }

    private void InitializeStatGenerator()
    {
        var statsByType = (Dictionary<StatType, StatData>)persistentData.StatsByType;

        var attributesByType = (Dictionary<AttributeType, AttributeData>)persistentData.AttributesByType;

        statGenerator = new StatGenerator(statsByType, attributesByType);
    }
    #endregion


    #endregion



}
