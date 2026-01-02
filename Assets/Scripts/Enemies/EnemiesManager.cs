using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Level level;

    [SerializeField] float enemyActivationDelay = 0.5f;

    float enemyActivationTime;
    List<Enemy> enemies;

    void Awake()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        foreach (var enemy in enemies){
            enemy.manager = this;
        }
        enemyActivationTime = Time.time + enemyActivationDelay;
    }

    void Update()
    {
        if (Time.time >= enemyActivationTime)
        {
            foreach (var enemy in enemies){
                enemy.SetState(EnemyState.Active);
            }
            enemyActivationTime = Mathf.Infinity;
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
