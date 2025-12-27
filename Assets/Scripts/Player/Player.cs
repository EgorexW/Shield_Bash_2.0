using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    
    public void Teleport(Vector3 transformPosition)
    {
        transform.position = transformPosition;
    }
    
    public Transform GetCacheParent()
    {
        return levelManager.GetPlayerCacheParent();
    }
}