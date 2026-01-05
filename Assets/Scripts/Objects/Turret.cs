using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour, ICacheRequester
{
    [BoxGroup("References")][Required][SerializeField] Transform shootPoint;
    
    public List<GameObject> teammates;
    
    [SerializeField] public bool autoShoot;

    public TurretStats stats;

    [FoldoutGroup("Events")] public UnityEvent beforeShoot;

    float nextShootTime;
    Transform cacheParent;

    void Awake()
    {
        nextShootTime = Time.time + stats.cooldownTime;
    }

    public void Update()
    {
        if (!autoShoot){
            return;
        }
        Shoot();
        if (Time.time > nextShootTime){
            return;
        }
        for (int i = 0; i < General.Iterationlimit; i++){
            if (!autoShoot){
                break;
            }
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time < nextShootTime)
        {
            return;
        }
        
        beforeShoot.Invoke();

        var targetRotation = shootPoint.rotation;
        var spreadAngle = UnityEngine.Random.Range(-stats.spread / 2f, stats.spread / 2f);
        targetRotation = targetRotation * Quaternion.Euler(0f, 0f, spreadAngle);
        
        var bullet = Instantiate(stats.bulletPrefab, shootPoint.position, targetRotation, cacheParent).GetComponent<Bullet>();
        if (stats.bulletStats){
            bullet.stats = stats.bulletStats;
        }
        bullet.IgnoreGameObject(gameObject);
        bullet.IgnoreGameObjects(teammates);
        nextShootTime = Time.time + stats.cooldownTime;
    }

    public void SetCacheParent(Transform parent)
    {
        cacheParent = parent;
    }
}

[Serializable][BoxGroup("Turret Stats")][InlineProperty][HideLabel]
public class TurretStats
{
    [Required] public GameObject bulletPrefab;
    public float cooldownTime = 1f;
    public float spread = 0f;
    public Optional<BulletStats> bulletStats = new Optional<BulletStats>();
}
