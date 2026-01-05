using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleMovementHandler : EnemyMovementHandler
{
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;

    public override void Refresh() { }

    public override void SetTarget(Vector3 targetPosition, IEnemyMovementProvider provider)
    {
        characterMovement.SetTarget(targetPosition);
    }

    public override void SetMovementInput(Vector2 targetDir, IEnemyMovementProvider provider)
    {
        characterMovement.SetMovementInput(targetDir);
    }

    public override void SetMovementInputAndSpeed(Vector2 targetDir, float speed, IEnemyMovementProvider provider)
    {
        SetMovementInput(targetDir, provider);
        characterMovement.speed = speed;
    }
}