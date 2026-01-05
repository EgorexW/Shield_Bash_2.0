using Sirenix.OdinInspector;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] ObjectsFactory objectsPool;

    [BoxGroup("References")] [Required] [SerializeField] CharacterHealth characterHealth;

    void Start()
    {
        characterHealth.onDamage.AddListener(UpdateHealthUI);
        characterHealth.onHeal.AddListener(UpdateHealthUI);
        UpdateHealthUI(characterHealth.health);
    }

    void UpdateHealthUI(Health health)
    {
        var value = Mathf.RoundToInt(health.value);
        objectsPool.SetCount(value);
    }
}