using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShieldEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Shield shield;
    [BoxGroup("References")] [Required] [SerializeField] SpriteRenderer shieldFill;

    void Awake()
    {
        shield.onStartRaising.AddListener(UpdateFill);
        shield.onDamage.AddListener(UpdateFill);
        shield.onShoot.AddListener(OnShoot);
    }

    void OnShoot(Bullet bullet)
    {
        bullet.GetComponent<ShieldBullet>().shieldFill.color = shieldFill.color;
    }

    void UpdateFill()
    {
        float energyPercent = shield.Energy / shield.stats.energy;
        var color = shieldFill.color;
        color.a = energyPercent;
        shieldFill.color = color;
    }
}
