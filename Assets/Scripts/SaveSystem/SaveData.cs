using System;
using UnityEngine.Serialization;

[Serializable]
public class SaveData
{
    public AbilitiesData abilitiesData;
    public LevelData levelData;
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