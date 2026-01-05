using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMantainDistanceMovement : MonoBehaviour, IEnemyMovementProvider
{
    [BoxGroup("References")] [Required] [SerializeField] Enemy enemy;
    [BoxGroup("References")] [Required] [SerializeField] EnemyMovementHandler movementHandler;

    [SerializeField] Vector2 mantainDistance = new(7f, 10f);
    [SerializeField] float changeDirChance = 1f;

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
        Vector2 dir = target.position - transform.position;
        if (dir.sqrMagnitude < mantainDistance.x * mantainDistance.x){
            var targetDir = -dir.normalized;
            movementHandler.SetMovementInput(targetDir, this);
            return;
        }
        if (dir.sqrMagnitude > mantainDistance.y * mantainDistance.y){
            movementHandler.SetMovementInput(dir, this);
            return;
        }
        if (Random.value < changeDirChance * Time.deltaTime){
            movementHandler.SetMovementInput(General.RandomPointOnCircle(), this);
        }
    }
}