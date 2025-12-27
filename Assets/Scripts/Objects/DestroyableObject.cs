using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDamageable
{
    [SerializeField] Health health;
    
    public void TakeDamage(Damage damage)
    {
        health.Damage(damage);
        if (!health.isAlive){
            Destroy(gameObject);
        }
    }
}
