using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public Health health;
    [SerializeField] bool destroyOnDeath = true;

    [FoldoutGroup("Events")] public UnityEvent<Health> onDamage;

    [FoldoutGroup("Events")] public UnityEvent<Health> onHeal;

    [FoldoutGroup("Events")] public UnityEvent<Health> onDeath;

    public void TakeDamage(Damage damage)
    {
        health.Damage(damage);
        onDamage.Invoke(health);
        if (health.isAlive){
            return;
        }
        Die();
    }

    void Die()
    {
        onDeath.Invoke(health);
        if (destroyOnDeath){
            Destroy(gameObject);
        }
        // BroadcastMessage("OnDeath", SendMessageOptions.RequireReceiver);
    }

    public void Heal(float amount = Mathf.Infinity)
    {
        health.Heal(amount);
        onHeal.Invoke(health);
    }
}