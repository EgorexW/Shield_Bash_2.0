using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Enemy enemy;
    [BoxGroup("References")][Required][SerializeField] CharacterHealth characterHealth;
    [BoxGroup("References")] [SerializeField] SpriteRenderer spriteRenderer;
    [BoxGroup("References")] [SerializeField] Collider2D collider2D;

    void Awake()
    {
        characterHealth.onDeath.AddListener(OnDeath);
    }

    void OnDeath(Health arg0)
    {
        PlayDead();
    }

    public void PlayDead()
    {
        enemy.SetState(EnemyState.Dead);
        if (collider2D != null){
            collider2D.enabled = false;
        }
        if (spriteRenderer != null){
            spriteRenderer.color = Color.gray;
        }
    }
}
