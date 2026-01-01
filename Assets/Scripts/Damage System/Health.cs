using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable][BoxGroup("Health")][InlineProperty][HideLabel]
public class Health
{
    public float value = 1;
    public float maxValue = 1;
    
    public bool isAlive => value > 0;

    public void Damage(Damage damage)
    {
        value -= damage.value;
        value = Mathf.Clamp(value, 0, maxValue);
    }

    public void Heal(float amount)
    {
        value += amount;
        value = Mathf.Clamp(value, 0, maxValue);
    }
}