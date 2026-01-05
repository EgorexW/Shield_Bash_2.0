using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShootBulletsBehaviour : EnemyBehaviour
{
    [BoxGroup("References")][Required][SerializeField] EnemyControlTurret turretControl;
    [SerializeField] TurretStats stats;
    [SerializeField] float bulletsNr;
    [SerializeField] float windUp = 0f;

    float startShootingTime = Mathf.Infinity;
    
    void Awake()
    {
        turretControl.onRunOutOfBullets.AddListener(BulletsEnded);
    }

    void Update()
    {
        if (!behaviourActive){
            return;
        }
        if (Time.time < startShootingTime){
            return;
        }
        StartShooting();
    }

    public override void StartBehaviour()
    {
        base.StartBehaviour();
        startShootingTime = Time.time + windUp;
    }

    void StartShooting()
    {
        startShootingTime = Mathf.Infinity;
        turretControl.TurretStats = stats;
        turretControl.ShootBullets(bulletsNr);
    }

    void BulletsEnded()
    {
        if (behaviourActive){
            enemyBehaviours.BehaviourEnded(this);
        }
    }

    public override void EndBehaviour()
    {
        base.EndBehaviour();
        turretControl.StopShooting();
    }
    
    
}