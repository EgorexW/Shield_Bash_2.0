using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class DashBehaviour : EnemyBehaviour, IEnemyMovementProvider
{
    [BoxGroup("References")] [Required] [SerializeField] EnemyMovementHandler movementHandler;
    [BoxGroup("References")] [Required] [SerializeField] Enemy enemy;
    
    [SerializeField] float speed = 10f;
    [SerializeField] float maxDistance = 10f;

    Vector2 targetPos;
    float endTime;

    void Awake()
    {
        movementHandler.Register(this);
    }

    void Update()
    {
        if (!behaviourActive){
            return;
        }
        movementHandler.SetTarget(enemy.GetTargetPosition(), this);
        if (Time.time > endTime){
            enemyBehaviours.BehaviourEnded(this);
        }
    }

    public override void StartBehaviour()
    {
        base.StartBehaviour();
        movementHandler.Refresh();
        targetPos = enemy.GetTargetPosition();
        Vector2 targetDir = (targetPos - (Vector2)transform.position).normalized;
        var distance = maxDistance;
        endTime = Time.time + distance/speed;
        Debug.DrawLine(transform.position, (Vector2)transform.position + targetDir * distance, Color.yellow, 1);
        movementHandler.SetMovementInputAndSpeed(targetDir, speed, this);
    }

    public override void EndBehaviour()
    {
        base.EndBehaviour();
        movementHandler.Refresh();
    }

    public float GetPriority()
    {
        return behaviourActive ? 2 : 0;
    }

    public void Refresh()
    {
        // No additional state to refresh
    }
}