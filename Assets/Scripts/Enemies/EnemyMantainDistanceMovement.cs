using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyMantainDistanceMovement : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;
    
    [SerializeField] Vector2 mantainDistance = new Vector2(7f, 10f);
    [SerializeField] float changeDirChance = 1f;

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
        Vector2 dir = target.position - transform.position;
        if (dir.sqrMagnitude < mantainDistance.x * mantainDistance.x){
            var targetDir = -dir.normalized;
            characterMovement.SetMovementInput(targetDir);
            return;
        }
        if (dir.sqrMagnitude > mantainDistance.y * mantainDistance.y){
            characterMovement.SetMovementInput(dir);
            return;
        }
        if (Random.value < changeDirChance * Time.deltaTime){
            characterMovement.SetMovementInput(General.RandomPointOnCircle());
        }
    }
}
