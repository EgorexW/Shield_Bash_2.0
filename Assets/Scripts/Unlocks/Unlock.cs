using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] PlayerTrigger playerTrigger;

    void Awake()
    {
        playerTrigger.onPlayerEntered.AddListener(SaveUnlock);
    }

    void SaveUnlock(Player player)
    {
        // TODO saving unlock state
        UnlockEffect(player);
    }

    protected virtual void UnlockEffect(Player player)
    {
        
    }

    void Reset()
    {
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
    }
}