using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("level")] [BoxGroup("References")] public EnemiesManager manager;
    [FoldoutGroup("Events")] public UnityEvent<EnemyState> onChangeState;
    EnemyState state = EnemyState.Inactive;

    void Start()
    {
        if (manager == null){
            Debug.LogError("Level reference is not set for Enemy: " + gameObject.name, this);
        }
    }

    void OnDisable()
    {
        manager.EnemyDisabled(this);
    }

    public Transform GetTarget()
    {
        return GetTargets().Random(); // TODO: add target selection logic
    }

    public List<Transform> GetTargets()
    {
        return manager.GetTargets();
    }

    public List<Enemy> GetTeammates()
    {
        return manager.GetTeammates();
    }

    public void SetState(EnemyState newState)
    {
        if (state == newState){
            return;
        }
        state = newState;
        onChangeState?.Invoke(state);
    }

    public EnemyState GetState()
    {
        return state;
    }
}

public static class EnemyExtensions
{
    public static Vector3 GetTargetPosition(this Enemy enemy)
    {
        return enemy.GetTarget().transform.position;
    }
}

public enum EnemyState
{
    Inactive,
    Active
}