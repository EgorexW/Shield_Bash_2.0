using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour, ICacheRequester
{
    [BoxGroup("References")][Required][SerializeField] GameObject bulletPrefab;
    [BoxGroup("References")][Required][SerializeField] Transform shootPoint;

    public List<GameObject> teammates;
    
    [SerializeField] float cooldownTime = 1f;
    [SerializeField] public bool autoShoot;

    [FoldoutGroup("Events")] public UnityEvent beforeShoot;

    float nextShootTime;
    Transform cacheParent;

    void Awake()
    {
        nextShootTime = Time.time + cooldownTime;
    }

    public void Update()
    {
        if (autoShoot){
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time < nextShootTime)
        {
            return;
        }
        
        beforeShoot?.Invoke();
        
        var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, cacheParent).GetComponent<Bullet>();
        bullet.IgnoreGameObject(gameObject);
        bullet.IgnoreGameObjects(teammates);
        nextShootTime = Time.time + cooldownTime;
    }

    public void SetCacheParent(Transform parent)
    {
        cacheParent = parent;
    }
}
