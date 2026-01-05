using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class AdvancedMovementHandler : EnemyMovementHandler
{
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;
    
    [SerializeField] float defaultSpeed = -1;
    
    IEnemyMovementProvider activeMovementProvider;

    void Awake()
    {
        if (defaultSpeed < 0){
            defaultSpeed = characterMovement.speed;
        }
    }

    public override void Refresh()
    {
        float highestPriority = 0;
        movementProviders.Shuffle();
        foreach (var movementProvider in movementProviders){
            var priority = movementProvider.GetPriority();
            if (priority > highestPriority){
                highestPriority = priority;
                activeMovementProvider = movementProvider;
            }
        }
    }

    public override void SetTarget(Vector3 targetPosition, IEnemyMovementProvider provider)
    {
        if (provider != activeMovementProvider){
            return;
        }
        SetDefaultSpeed();
        characterMovement.SetTarget(targetPosition);
    }

    public override void SetMovementInput(Vector2 targetDir, IEnemyMovementProvider provider)
    {
        if (provider != activeMovementProvider){
            return;
        }
        SetDefaultSpeed();
        characterMovement.SetMovementInput(targetDir);
    }

    public override void SetMovementInputAndSpeed(Vector2 targetDir, float speed, IEnemyMovementProvider provider)
    {
        if (provider != activeMovementProvider){
            return;
        }
        SetMovementInput(targetDir, provider);
        characterMovement.speed = speed;
    }

    void SetDefaultSpeed()
    {
        if (defaultSpeed < 0){
            Debug.LogWarning("Default speed not set for AdvancedMovementHandler", this);
            return;
        }
        characterMovement.speed = defaultSpeed;
    }
}