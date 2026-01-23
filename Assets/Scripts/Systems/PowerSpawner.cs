using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

// AI
public class PowerSpawner : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Level level;
    // Changed to Component to ensure it's a GameObject, but logic handles Interface
    [BoxGroup("References")] [Required] [SerializeField] GameObject healPrefab;
    [BoxGroup("References")] [Required] [SerializeField] Collider2D bounds;

    [BoxGroup("Settings")] [SerializeField] float trySpawnInterval = 1f;
    [BoxGroup("Settings")] [SerializeField] float normalSpawnChance = 0.1f;
    [BoxGroup("Settings")] [SerializeField] float lowHealthSpawnChance = 0.2f;
    [BoxGroup("Settings")] [SerializeField] int maxSpawnedPowers = 1;
    
    // Track references to valid IPower components
    private List<IPower> activePowers = new List<IPower>();
    private Coroutine spawnRoutine;

    void Awake()
    {
        bounds.isTrigger = true;
    }

    void Start()
    {
        // Start the logic loop
        spawnRoutine = StartCoroutine(SpawnCheckRoutine());
    }

    // Using a Coroutine is much cleaner for interval-based logic than Update
    IEnumerator SpawnCheckRoutine()
    {
        var wait = new WaitForSeconds(trySpawnInterval);

        while (true)
        {
            yield return wait;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // 1. Clean up list (remove nulls if objects were destroyed unexpectedly)
        activePowers.RemoveAll(p => p == null || p.Equals(null));

        // 2. Check limits
        if (activePowers.Count >= maxSpawnedPowers) return;

        // 3. Check Player Health
        // Cache this reference if possible, otherwise this null check prevents crashes if player dies
        var player = level.levelReference.GetPlayer();
        if (player == null) return;

        var healthData = player.CharacterHealth.health; // Assuming this is a struct or class
        if (healthData.isMax) return;

        // 4. RNG Logic
        var spawnChance = healthData.value > 1 ? normalSpawnChance : lowHealthSpawnChance;
        if (Random.value > spawnChance) return;

        SpawnPower();
    }

    void SpawnPower()
    {
        Vector2 pos = General.RandomPointInsideCollider2D(bounds);
        
        // Instantiate
        GameObject newObj = Instantiate(healPrefab, pos, Quaternion.identity, level.levelReference.GetCacheParent());

        // Safe component retrieval
        if (newObj.TryGetComponent(out IPower power))
        {
            // Subscribe to event
            power.OnPowerDespawned.AddListener(OnPowerDespawned);
            activePowers.Add(power);
        }
        else
        {
            Debug.LogError($"Prefab {healPrefab.name} does not implement IPower!", this);
            Destroy(newObj); // Cleanup to prevent clutter
        }
    }

    void OnPowerDespawned(IPower power)
    {
        power.OnPowerDespawned.RemoveListener(OnPowerDespawned);
        activePowers.Remove(power);
    }
}

public interface IPower
{
    // Renamed to avoid confusion with Unity's OnDestroy
    UnityEvent<IPower> OnPowerDespawned { get; }
}