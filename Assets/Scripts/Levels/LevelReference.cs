using Sirenix.OdinInspector;
using UnityEngine;

public class LevelReference : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    
    public void ExitToLevel(GameObject nextLevel)
    {
        levelManager.LoadLevel(nextLevel);
    }
}