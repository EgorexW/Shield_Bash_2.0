using Sirenix.OdinInspector;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    [BoxGroup("References")][Required][SerializeField] GameObject startingLevel;
    
    void Start()
    {
        levelManager.LoadLevel(startingLevel);
    }
}