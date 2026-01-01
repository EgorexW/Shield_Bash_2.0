using Sirenix.OdinInspector;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    // [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    // [BoxGroup("References")][Required][SerializeField] LevelLoadInfo startingLevel;
    [BoxGroup("References")][Required][SerializeField] SaveSystem saveSystem;
    
    void Start()
    {
        saveSystem.Load();
        // levelManager.LoadLevel(startingLevel);
    }
}