using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Rigidbody2D rb;

    [SerializeField] BulletStats bulletStats;
    
    List<GameObject> ignoreObjects = new();
    float deathTime;

    void Start()
    {
        deathTime = Time.time + bulletStats.lifeTime;
    }

    void FixedUpdate()
    {
        Vector2 movement = rb.transform.up;
        Vector2 move = movement * (bulletStats.speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + move);
    }

    void Update()
    {
        if (Time.time >= deathTime)
        {
            Destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoreObjects.Contains(other.gameObject))
        {
            return;
        }
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(bulletStats.damage);
        }
        if (bulletStats.pierce){
            return;
        }
        Destroy();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void IgnoreGameObject(GameObject obj)
    {
        ignoreObjects.Add(obj);
    }

    public void IgnoreGameObjects(List<GameObject> objs)
    {
        ignoreObjects.AddRange(objs);
    }
}

[Serializable][BoxGroup("Bullet Stats")][InlineProperty][HideLabel]
public class BulletStats
{
    public float speed = 200f;
    public float lifeTime = 5f;
    public bool pierce = false;
    public Damage damage = new Damage();
}