using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("level")] [BoxGroup("References")] public EnemiesManager manager;

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
}
