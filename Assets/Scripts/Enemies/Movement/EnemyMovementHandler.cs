using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementHandler : MonoBehaviour
{
    protected List<IEnemyMovementProvider> movementProviders = new();

    public virtual void Register(IEnemyMovementProvider movementProvider)
    {
        movementProviders.Add(movementProvider);
        Refresh();
    }

    public abstract void Refresh();

    public abstract void SetTarget(Vector3 targetPosition, IEnemyMovementProvider provider);

    public abstract void SetMovementInput(Vector2 targetDir, IEnemyMovementProvider provider);
    public abstract void SetMovementInputAndSpeed(Vector2 targetDir, float speed, IEnemyMovementProvider provider);
}

public interface IEnemyMovementProvider
{
    float GetPriority()
    {
        return 1;
    }

    void Refresh();
}