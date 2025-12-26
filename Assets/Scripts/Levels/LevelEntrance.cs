using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    public LevelEntranceIndex entranceIndex;

    public void TeleportPlayerToEntrance(LevelReference levelReference)
    {
        levelReference.SetPlayerPosition(transform.position);
    }
}

public enum LevelEntranceIndex
{
    Left1,
    Left2,
    Right1,
    Right2,
    Top1,
    Top2,
    Bottom1,
    Bottom2
}
