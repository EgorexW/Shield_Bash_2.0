using Sirenix.OdinInspector;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] PlayerTrigger playerTrigger;

    void Start()
    {
        playerTrigger?.onPlayerEntered.AddListener(Save);
    }

    void Save(Player player)
    {
        player.CharacterHealth.Heal();
        player.SaveGame();
    }
}