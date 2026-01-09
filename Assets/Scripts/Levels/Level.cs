using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class Level : MonoBehaviour
{
    [BoxGroup("References")][GetComponent][SerializeField] PrefabListIndexHolder prefabListIndexHolder;
    [BoxGroup("References")] [Required] [SerializeField] ExitsManager exitsManager;

    [SerializeField] float levelSize = 10;
    
    [HideInInspector] public LevelReference levelReference;
    public float LevelSize => levelSize;
    
    public void Unload()
    {
        // Implement level unloading logic here
        Destroy(gameObject);
    }

    public void Load(LevelLoadInfo levelInfo, LevelReference levelReferenceTmp)
    {
        foreach (var levelObject in GetComponentsInChildren<ILevelObject>()) levelObject.parentLevel = this;
        levelReference = levelReferenceTmp;
        var levelEntrances = GetComponentsInChildren<LevelEntrance>();
        var entrance = levelEntrances.First(le => le.entranceIndex == levelInfo.entranceIndex);
        foreach (var cacheRequester in GetComponentsInChildren<ICacheRequester>())
            cacheRequester.SetCacheParent(levelReference.GetCacheParent());
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

    public ExitsManager GetExitsManager()
    {
        return exitsManager;
    }
}

public interface ICacheRequester
{
    void SetCacheParent(Transform parent);
}