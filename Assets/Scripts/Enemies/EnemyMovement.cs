using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;

    void Update()
    {
        var target = enemy.GetTarget();
        if (target == null){
            return;
        }
        characterMovement.SetTarget(target.position);
        Vector2 targetDir = (target.position - transform.position).normalized;
        characterMovement.SetMovementInput(targetDir);
    }
}
