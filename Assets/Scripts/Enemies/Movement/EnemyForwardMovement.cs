using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyForwardMovement : MonoBehaviour, IEnemyMovementProvider
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    [BoxGroup("References")] [Required] [SerializeField] EnemyMovementHandler movementHandler;

    void Awake()
    {
        movementHandler.Register(this);
    }

    void Update()
    {
        if (enemy.GetState() != EnemyState.Active){
            return;
        }
        var target = enemy.GetTarget();
        if (target == null){
            return;
        }
        movementHandler.SetTarget(target.position, this);
        Vector2 targetDir = (target.position - transform.position).normalized;
        movementHandler.SetMovementInput(targetDir, this);
    }
}
