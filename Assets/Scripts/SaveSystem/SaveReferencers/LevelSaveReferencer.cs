using Sirenix.OdinInspector;
using UnityEngine;

public class LevelSaveReferencer : SaveReferencer
{
    [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    
    public override void OnSave(SaveData data)
    {
        data.levelData.levelIndex = levelManager.GetLoadedLevel().GetPrefabListIndex();
    }
    public override void OnLoad(SaveData data)
    {
        LevelLoadInfo info = new LevelLoadInfo(){
            nextLevelIndex = data.levelData.levelIndex,
            entranceIndex = LevelEntranceIndex.LoadFromSave
        };
        levelManager.LoadLevel(info);
    }
}