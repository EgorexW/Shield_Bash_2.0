using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyDamageOnContact : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Enemy enemy;
    
    [SerializeField] Damage damage;
    [SerializeField] bool onlyTargets = true;
    [SerializeField] float cooldown = 2f;
    
    float lastDamageTime = -Mathf.Infinity;

    void OnCollisionStay2D(Collision2D other)
    {
        if (Time.time - lastDamageTime < cooldown){
            return;
        }
        lastDamageTime = Time.time;
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable == null)
        {
            return;
        }
        if (onlyTargets){
            if (!enemy.GetTargets().Contains(other.transform)){
                return;
            }
        }
        damageable.TakeDamage(damage);
    }
}
