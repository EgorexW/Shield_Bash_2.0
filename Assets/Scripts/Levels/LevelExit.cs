using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelExit : LevelObject
{
    [BoxGroup("References")] [Required] [SerializeField] new Collider2D collider;
    [BoxGroup("References")] [Required] [SerializeField] GameObject blockedEffect;

    [SerializeField] LevelLoadInfo loadInfo;

    void Awake()
    {
        SetBlock(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = General.GetComponentFromCollider<Player>(other);
        if (player != null){
            parentLevel.MoveToLevel(loadInfo);
        }
    }

    public void SetBlock(bool block)
    {
        collider.isTrigger = !block;
        blockedEffect.SetActive(block);
    }
}

[Serializable]
public class LevelLoadInfo
{
    public GameObject nextLevel;
    [FormerlySerializedAs("levelIndex")] [HideIf("@nextLevel != null")] public int nextLevelIndex;
    public LevelEntranceIndex entranceIndex = LevelEntranceIndex.Default;
}