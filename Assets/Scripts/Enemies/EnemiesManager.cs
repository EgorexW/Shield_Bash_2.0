using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Level level;
    
    List<Enemy> enemies;

    void Awake()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        foreach (var enemy in enemies){
            enemy.manager = this;
        }
    }
    
    public List<Transform> GetTargets()
    {
        return new(){level.levelReference.GetPlayer().transform};
    }
    
    public List<Enemy> GetTeammates()
    {
        return enemies.Copy();
    }
}
