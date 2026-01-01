using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LevelReference levelReference;
     [SerializeField][GetComponent] PrefabListIndexHolder prefabListIndexHolder;
    
    public void Unload()
    {
        // Implement level unloading logic here
        Destroy(gameObject);
    }

    public void Load(LevelLoadInfo levelInfo, LevelReference levelReferenceTmp)
    {
        foreach (var levelObject in GetComponentsInChildren<ILevelObject>())
        {
            levelObject.parentLevel = this;
        }
        levelReference = levelReferenceTmp;
        var levelEntrances = GetComponentsInChildren<LevelEntrance>();
        var entrance = levelEntrances.First(le => le.entranceIndex == levelInfo.entranceIndex);
        foreach (var cacheRequester in GetComponentsInChildren<ICacheRequester>()){
            cacheRequester.SetCacheParent(levelReference.GetCacheParent());
        }
        entrance.TeleportPlayerToEntrance(levelReference);
    }

    public void MoveToLevel(LevelLoadInfo loadInfo)
    {
        levelReference.MoveToLevel(loadInfo);
    }
    
    public int GetPrefabListIndex()
    {
        return prefabListIndexHolder.prefabListIndex;
    }
}

public interface ICacheRequester
{
    void SetCacheParent(Transform parent);
}