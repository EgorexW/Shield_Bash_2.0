using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelReference levelReference;
    
    [ReadOnly][SerializeField] Level loadedLevel;
    
    [Button("Load Level")]
    public void LoadLevel(LevelLoadInfo levelInfo)
    {
        if (loadedLevel != null)
        {
            UnloadLevel();
        }
        
        loadedLevel = Instantiate(levelInfo.nextLevel, transform).GetComponent<Level>();
        Debug.Assert(loadedLevel != null, "Loaded level does not contain a Level component");
        loadedLevel.Load(levelInfo, levelReference);
    }

    void UnloadLevel()
    {
        if (loadedLevel == null){
            return;
        }
        loadedLevel.Unload();
        loadedLevel = null;
    }
}