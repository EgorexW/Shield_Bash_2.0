using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SaveData
{
    public AbilitiesData abilitiesData;
    public LevelData levelData;
    public TriggersData triggersData;
}

[Serializable]
public class TriggersData
{
    [SerializeField] List<SaveTriggerType> triggerTypes = new ();
    [SerializeField] List<bool> values = new ();
    
    public void SetTrigger(SaveTriggerType triggerType, bool triggered)
    {
        triggerTypes.Add(triggerType);
        values.Add(triggered);
    }

    public bool GetTrigger(SaveTriggerType triggerType)
    {
        var index = triggerTypes.IndexOf(triggerType);
        if (index == -1)
        {
            return false;
        }
        return values[index];
    }   
}

[Serializable]
public class LevelData
{
    [FormerlySerializedAs("currentLevelBuildIndex")] public int levelIndex;
}

[Serializable]
public class AbilitiesData
{
    public bool pierce;
    public bool canShoot;
    public bool canBlock;
}