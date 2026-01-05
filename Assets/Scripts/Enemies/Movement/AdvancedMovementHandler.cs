using Sirenix.OdinInspector;
using UnityEngine;

public class AdvancedMovementHandler : EnemyMovementHandler
{
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;
    
    IEnemyMovementProvider activeMovementProvider;
        
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
            characterMovement.SetTarget(targetPosition);
        }
        public override void SetMovementInput(Vector2 targetDir, IEnemyMovementProvider provider)
        {
            if (provider != activeMovementProvider){
                return;
                }
            characterMovement.SetMovementInput(targetDir);
        }
    }