using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    [BoxGroup("References")][Required][SerializeField] Turret turret;

    void Start()
    {
        turret.beforeShoot.AddListener(BeforeShoot);
        enemy.onChangeState.AddListener(OnChangeState);
    }

    void OnChangeState(EnemyState arg0)
    {
        turret.autoShoot = arg0 == EnemyState.Active;
    }

    void BeforeShoot()
    {
        turret.teammates = enemy.GetTeammates().ConvertAll(e => e.gameObject);
    }

    void OnDestroy()
    {
        turret.beforeShoot.RemoveListener(BeforeShoot);
    }
}
