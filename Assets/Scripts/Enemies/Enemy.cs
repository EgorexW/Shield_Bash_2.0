using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("level")] [BoxGroup("References")] public EnemiesManager manager;
    EnemyState state = EnemyState.Inactive;
    [FoldoutGroup("Events")] public UnityEvent<EnemyState> onChangeState;

    void Start()
    {
        if (manager == null)
        {
            Debug.LogError("Level reference is not set for Enemy: " + gameObject.name, this);
        }
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

public enum EnemyState
{
    Inactive,
    Active,
}
