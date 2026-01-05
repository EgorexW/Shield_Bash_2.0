using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyForwardMovement : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;

    void Update()
    {
        if (enemy.GetState() != EnemyState.Active){
            return;
        }
        var target = enemy.GetTarget();
        if (target == null){
            return;
        }
        characterMovement.SetTarget(target.position);
        Vector2 targetDir = (target.position - transform.position).normalized;
        characterMovement.SetMovementInput(targetDir);
    }
}
