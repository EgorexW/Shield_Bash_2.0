using UnityEngine;

public class Level : MonoBehaviour
{
    LevelReference levelReference;
    
    public void Unload()
    {
        // Implement level unloading logic here
        Destroy(gameObject);
    }

    public void Load(LevelReference levelReferenceTmp)
    {
        foreach (var levelObject in GetComponentsInChildren<ILevelObject>())
        {
            levelObject.parentLevel = this;
        }
        levelReference = levelReferenceTmp;
    }

    public void ExitToLevel(GameObject nextLevel)
    {
        // Implement level transition logic here
        Debug.Log($"Exiting to level: {nextLevel.name}");
        levelReference.ExitToLevel(nextLevel);
    }
}