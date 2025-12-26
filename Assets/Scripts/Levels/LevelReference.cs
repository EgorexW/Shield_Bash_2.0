using Sirenix.OdinInspector;
using UnityEngine;

public class LevelReference : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LevelManager levelManager;
    [BoxGroup("References")][Required][SerializeField] Player player;

    public void MoveToLevel(LevelLoadInfo nextLevel)
    {
        levelManager.LoadLevel(nextLevel);
    }
    public void SetPlayerPosition(Vector3 position)
    {
        player.Teleport(position);
    }
}