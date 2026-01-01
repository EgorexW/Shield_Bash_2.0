using Sirenix.OdinInspector;
using UnityEngine;

public class DefaultSaveData : MonoBehaviour
{
    [SerializeField] SaveData defaultSaveData;
    
    public SaveData CreateDefaultSaveData()
    {
        return defaultSaveData;
    }
}