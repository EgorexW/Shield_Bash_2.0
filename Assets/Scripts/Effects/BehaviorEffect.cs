using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BehaviorEffect : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] EnemyBehaviour behaviour;
    
    [BoxGroup("References")][SerializeField] List<ParticleSystem> particles;

    void Awake()
    {
        behaviour.onStartBehaviour.AddListener(ShowEffect);
        behaviour.onEndBehaviour.AddListener(EndEffect);
    }

    void ShowEffect()
    {
        foreach (var particle in particles){
            particle.Play();
        }
    }

    void EndEffect()
    {
        foreach (var particle in particles){
            particle.Stop();
        }
    }

    void OnValidate()
    {
        foreach (var particle in particles){
            var main = particle.main;
            main.playOnAwake = false;
        }
    }

    void Reset()
    {
        behaviour = GetComponentInParent<EnemyBehaviour>();
        particles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        OnValidate();
    }
}
