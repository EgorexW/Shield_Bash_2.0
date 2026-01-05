using Sirenix.OdinInspector;
using UnityEngine;

public class LevelReference : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelManager levelManager;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [Required] [SerializeField] SaveSystem saveSystem;

    public void MoveToLevel(LevelLoadInfo nextLevel)
    {
        levelManager.LoadLevel(nextLevel);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        player.Teleport(position);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Transform GetCacheParent()
    {
        return levelManager.GetLevelCacheParent();
    }

    public void SaveGame()
    {
        saveSystem.Save();
    }
}