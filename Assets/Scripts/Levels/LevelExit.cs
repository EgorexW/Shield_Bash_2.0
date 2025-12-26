using Sirenix.OdinInspector;
using UnityEngine;

public class LevelExit : LevelObject
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject nextLevel;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = General.GetComponentFromCollider<Player>(other);
        if (player != null)
        {
            parentLevel.ExitToLevel(nextLevel);
        }
    }
}