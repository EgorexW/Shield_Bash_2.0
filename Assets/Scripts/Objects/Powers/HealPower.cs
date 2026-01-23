using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class HealPower : MonoBehaviour, IPower
{
    [BoxGroup("References")] [Required] [SerializeField] PlayerTrigger playerTrigger;

    public float healAmount = 1;
        
    void Awake()
    {
        playerTrigger.onPlayerEntered.AddListener(Heal);
    }

    void Heal(Player player)
    {
        player.CharacterHealth.Heal(healAmount);
        OnPowerDespawned.Invoke(this);
        Destroy(gameObject);
    }

    public UnityEvent<IPower> OnPowerDespawned{ get; } = new();
}
