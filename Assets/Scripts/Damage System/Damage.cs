using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable][BoxGroup("Damage")][InlineProperty][HideLabel]
public class Damage
{
    public float value = 1;
}

public interface IDamageable
{
    public void TakeDamage(Damage damage);
}
