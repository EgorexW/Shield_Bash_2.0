using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField][ReadOnly] bool held = false;
    [SerializeField][ReadOnly] ShieldState state;
    
    [BoxGroup("References")][Required][SerializeField] Player player;
    
    [BoxGroup("References")][Required][SerializeField] Transform shieldTransform;
    [BoxGroup("References")][Required][SerializeField] Collider2D shieldCollider;
    [BoxGroup("References")][Required][SerializeField] GameObject bulletPrefab;
    
    [SerializeField] public ShieldStats stats;


    void Awake()
    {
        Hide();
    }

    public void Raise()
    {
        held = true;
    }

    public void Release()
    {
        held = false;
    }
    
    void Update()
    {
        switch (state)
        {
            case ShieldState.Idle:
                UpdateIdle();
                break;
            case ShieldState.Raising:
                UpdateRaising();
                break;
            case ShieldState.Raised:
                UpdateRaised();
                break;
            case ShieldState.Releasing:
                UpdateReleasing();
                break;
        }
    }

    void UpdateReleasing()
    {
        var scaleChange = Time.deltaTime / stats.hideTime;
        shieldTransform.localScale -= new Vector3(scaleChange, scaleChange, 0f);
        if (!(shieldTransform.localScale.x <= 0f)){
            return;
        }
        Hide();
    }

    void Hide()
    {
        shieldTransform.localScale = new Vector3(0f, 0f, 1f);
        state = ShieldState.Idle;
        shieldCollider.enabled = false;
    }

    void UpdateRaised()
    {
        if (!held || !stats.canBlock)
        {
            shieldCollider.enabled = true;
            state = ShieldState.Releasing;
            if (stats.canShoot){
                Shoot();
            }
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, shieldTransform.position, shieldTransform.rotation, player.GetCacheParent()).GetComponent<Bullet>();
        bullet.stats = stats.bulletStats;
        bullet.IgnoreGameObject(player.gameObject);
        Release();
        Hide();
    }

    void UpdateRaising()
    {
        var scaleChange = Time.deltaTime / stats.raiseTime;
        shieldTransform.localScale += new Vector3(scaleChange, scaleChange, 0f);
        if (!(shieldTransform.localScale.x >= 1f)){
            return;
        }
        Raised();
    }

    void Raised()
    {
        shieldTransform.localScale = new Vector3(1f, 1f, 1f);
        state = ShieldState.Raised;
        shieldCollider.enabled = true;
    }

    void UpdateIdle()
    {
        if (held)
        {
            state = ShieldState.Raising;
        }
    }
}

[Serializable][BoxGroup("Shield Stats")][InlineProperty][HideLabel]
public class ShieldStats
{
    [BoxGroup("Values")] public float raiseTime = 0.5f;
    [BoxGroup("Values")] public float hideTime = 0.5f;

    [BoxGroup("Abilities")] public bool canBlock;
    [BoxGroup("Abilities")] public bool canShoot;
    
    public BulletStats bulletStats;
}

enum ShieldState
{
    Idle,
    Raising,
    Raised,
    Releasing
}