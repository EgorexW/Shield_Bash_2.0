using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelExit : LevelObject
{
    [FormerlySerializedAs("movementInfo")] [BoxGroup("References")][Required][SerializeField] LevelLoadInfo loadInfo;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = General.GetComponentFromCollider<Player>(other);
        if (player != null)
        {
            parentLevel.MoveToLevel(loadInfo);
        }
    }
}

[Serializable]
public class LevelLoadInfo
{
    public GameObject nextLevel;
    public LevelEntranceIndex entranceIndex;
}