using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public Health health;
    [SerializeField] bool destroyOnDeath = true;

    [FoldoutGroup("Events")]
    public UnityEvent<Health> onDamage;
    
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
        if (destroyOnDeath){
            Destroy(gameObject);
            return;
        }
        Debug.LogError("Character has died.", this);
    }
}
