using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHealthEffects : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] CharacterHealth playerHealth;
    [BoxGroup("References")][SerializeField] ParticleSystem healEffect;
    [BoxGroup("References")][SerializeField] ParticleSystem damageEffect;
    
    void Awake()
    {
        playerHealth.onHeal.AddListener(OnHeal);
        playerHealth.onDamage.AddListener(OnDamage);
    }

    void OnDamage(Health arg0)
    {
        if (damageEffect != null)
        {
            damageEffect.Play();
        }
    }

    void OnHeal(Health arg0)
    {
        if (healEffect != null)
        {
            healEffect.Play();
        }
    }

    void OnValidate()
    {
        if (healEffect != null)
        {
            var main = healEffect.main;
            main.playOnAwake = false;
            main.loop = false;
        }
        if (damageEffect != null)
        {
            var main = damageEffect.main;
            main.playOnAwake = false;
            main.loop = false;
        }
    }
}
