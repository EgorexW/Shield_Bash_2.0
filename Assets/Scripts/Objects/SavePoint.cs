using Sirenix.OdinInspector;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Level level;
    [SerializeField] PlayerTrigger playerTrigger;

    void Start()
    {
        playerTrigger?.onPlayerEntered.AddListener(Save);
    }

    void Save(Player player)
    {
        player.CharacterHealth.Heal();
        level.levelReference.SaveGame();
    }
}