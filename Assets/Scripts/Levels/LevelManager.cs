using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelReference levelReference;
    
    [ReadOnly][SerializeField] Level loadedLevel;
    
    [Button("Load Level")]
    public void LoadLevel(GameObject level)
    {
        if (loadedLevel != null)
        {
            UnloadLevel();
        }
        
        loadedLevel = Instantiate(level, transform).GetComponent<Level>();
        Debug.Assert(loadedLevel != null, "Loaded level does not contain a Level component");
        loadedLevel.Load(levelReference);
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