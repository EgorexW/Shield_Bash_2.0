using System;
using UnityEngine;

public class ShieldUnlock : Unlock
{
    [SerializeField] ShieldUnlockType shieldUnlockType;

    protected override void UnlockEffect(Player player)
    {
        var shield = player.GetShield();
        switch (shieldUnlockType){
            case ShieldUnlockType.CanShoot:
                shield.stats.canShoot = true;
                Debug.Log("Shield can now shoot!", this);
                break;
            case ShieldUnlockType.CanBlock:
                shield.stats.canBlock = true;
                Debug.Log("Shield can now block!", this);
                break;
            case ShieldUnlockType.CanPierce:
                shield.stats.bulletStats.pierce = true;
                Debug.Log("Shield bullets can now pierce!", this);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

enum ShieldUnlockType
{
    CanShoot,
    CanBlock,
    CanPierce
}