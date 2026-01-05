using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Level level;

    [SerializeField] float enemyActivationDelay = 0.5f;
    [SerializeField] bool blockExits = true;
    List<Enemy> enemies;

    float enemyActivationTime;

    void Awake()
    {
        enemies = new List<Enemy>(GetComponentsInChildren<Enemy>());
        if (enemies.Count == 0){
            enabled = false;
            return;
        }
        foreach (var enemy in enemies) enemy.manager = this;
        enemyActivationTime = Time.time + enemyActivationDelay;
    }

    void Update()
    {
        if (Time.time >= enemyActivationTime){
            foreach (var enemy in enemies) enemy.SetState(EnemyState.Active);
            if (blockExits){
                level.GetExitsManager().SetBlock(true);
            }
            enemyActivationTime = Mathf.Infinity;
        }
    }

    public List<Transform> GetTargets()
    {
        return new List<Transform>{ level.levelReference.GetPlayer().transform };
    }

    public List<Enemy> GetTeammates()
    {
        return enemies.Copy();
    }

    public void EnemyDisabled(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 && blockExits){
            level.GetExitsManager().SetBlock(false);
            enabled = false;
        }
    }
}