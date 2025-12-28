using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public class Bullet : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Rigidbody2D rb;

    [FormerlySerializedAs("bulletStats")] [SerializeField] public BulletStats stats;
    
    List<GameObject> ignoreObjects = new();
    float deathTime;

    void Start()
    {
        deathTime = Time.time + stats.lifeTime;
    }

    void FixedUpdate()
    {
        Vector2 movement = rb.transform.up;
        Vector2 move = movement * (stats.speed * Time.fixedDeltaTime);
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
            damageable.TakeDamage(stats.damage);
        }
        if (stats.pierce){
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
    public float speed = 10f;
    public float lifeTime = 5f;
    public bool pierce = false;
    public Damage damage = new Damage();
}