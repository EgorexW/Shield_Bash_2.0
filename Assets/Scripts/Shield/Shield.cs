using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Shield : MonoBehaviour
{
    [SerializeField][ReadOnly] bool held = false;
    [SerializeField][ReadOnly] ShieldState state;
    
    [BoxGroup("References")][Required][SerializeField] Player player;
    
    [BoxGroup("References")][Required][SerializeField] Transform shieldTransform;
    [BoxGroup("References")][Required][SerializeField] Collider2D shieldCollider;
    [BoxGroup("References")][Required][SerializeField] GameObject bulletPrefab;
    [BoxGroup("References")][Required][SerializeField] CharacterHealth shieldHealth;
    
    [SerializeField] public ShieldStats stats;

    float energy;
    float cooldownEndTime;

    public float Energy => energy;

    [FormerlySerializedAs("onRaise")] [FoldoutGroup("Events")]
    public UnityEvent onRaised;

    [FoldoutGroup("Events")]
    public UnityEvent onDamage;

    [FoldoutGroup("Events")]
    public UnityEvent onStartRaising;

    [FoldoutGroup("Events")] public UnityEvent<Bullet> onShoot;


    void Awake()
    {
        Hide();
        shieldHealth.health.maxValue = stats.energy;
        energy = stats.energy;
        shieldHealth.onDamage.AddListener(OnDamage);
        shieldHealth.onDeath.AddListener(OnDeath);
    }

    void OnDeath(Health health)
    {
        Hide();
        energy = 0;
    }

    void OnDamage(Health health)
    {
        energy = shieldHealth.health.value;
        if (energy < stats.minEnergyToRaise){
            OnDeath(health);
        }
        onDamage.Invoke();
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
            case ShieldState.OnCooldown:
                UpdateOnCooldown();
                break;
        }
    }

    void UpdateOnCooldown()
    {
        if (Time.time >= cooldownEndTime)
        {
            state = ShieldState.Idle;
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
        onShoot.Invoke(bullet);
        Hide();
        cooldownEndTime = stats.cooldownTime + Time.time;
        state = ShieldState.OnCooldown;
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
        onRaised.Invoke();
        shieldHealth.health.value = energy;
        shieldTransform.localScale = new Vector3(1f, 1f, 1f);
        state = ShieldState.Raised;
        shieldCollider.enabled = true;
    }

    void UpdateIdle()
    {
        energy += stats.energyGainRate * Time.deltaTime;
        energy = Mathf.Min(energy, stats.energy);
        if (!held){
            return;
        }
        if (energy < stats.minEnergyToRaise){
            return;
        }
        state = ShieldState.Raising;
        onStartRaising.Invoke();
    }
}

[Serializable][BoxGroup("Shield Stats")][InlineProperty][HideLabel]
public class ShieldStats
{
    [BoxGroup("Values")] public float raiseTime = 0.5f;
    [BoxGroup("Values")] public float hideTime = 0.5f;
    [BoxGroup("Values")] public float cooldownTime = 0.5f;
    [BoxGroup("Values")] public float energy = 5f;
    [BoxGroup("Values")] public float energyGainRate = 2f;
    [BoxGroup("Values")] public float minEnergyToRaise = 1f;

    [BoxGroup("Abilities")] public bool canBlock;
    [BoxGroup("Abilities")] public bool canShoot;
    
    public BulletStats bulletStats;
}

enum ShieldState
{
    Idle,
    Raising,
    Raised,
    Releasing,
    OnCooldown
}