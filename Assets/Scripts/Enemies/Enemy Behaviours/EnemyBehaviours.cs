using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyBehaviours : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    
    [SerializeField] List<ObjectWithValue<EnemyBehaviour>> behaviours;
    
    EnemyBehaviour previousBehaviour;
    EnemyBehaviour activeBehaviour;
    
    float nextBehaviourTime = Mathf.Infinity;

    void Awake()
    {
        enemy.onChangeState.AddListener(OnChangeState);
    }

    void Update()
    {
        if (Time.time >= nextBehaviourTime)
        {
            StartBehaviour();
        }
    }

    void OnChangeState(EnemyState state)
    {
        if (state == EnemyState.Active){
            StartBehaviour();
        }
        else{
            EndBehaviour();
        }
    }

    void EndBehaviour()
    {
        if (activeBehaviour == null){
            return;
        }
        previousBehaviour = activeBehaviour;
        activeBehaviour.EndBehaviour();
        activeBehaviour = null;
    }

    void StartBehaviour()
    {
        EndBehaviour();
        activeBehaviour = SelectBehaviour();
        activeBehaviour.enemyBehaviours = this;
        nextBehaviourTime = Mathf.Infinity;
        activeBehaviour.StartBehaviour();
    }

    EnemyBehaviour SelectBehaviour()
    {
        var behavioursTmp = behaviours.Copy();
        if (previousBehaviour != null && !previousBehaviour.CanRepeat){
            behavioursTmp.RemoveAll(behaviour => behaviour.Object == previousBehaviour);
        }
        var behaviour = behavioursTmp.GetWeightedRoll();
        return behaviour;
    }
    
    void Reset()
    {
        behaviours = new List<ObjectWithValue<EnemyBehaviour>>();
        foreach (EnemyBehaviour behaviour in GetComponents<EnemyBehaviour>())
        {
            behaviours.Add(new ObjectWithValue<EnemyBehaviour>(1f, behaviour));
        }
    }

    public void BehaviourEnded(EnemyBehaviour enemyBehaviour)
    {
        if (activeBehaviour != enemyBehaviour){
            Debug.LogError("Enemy behaviour ended that is not active!", this);
            return;
        }
        if (enemy.GetState() != EnemyState.Active){
            return;
        }
        nextBehaviourTime = Time.time + enemyBehaviour.Cooldown;
    }
}