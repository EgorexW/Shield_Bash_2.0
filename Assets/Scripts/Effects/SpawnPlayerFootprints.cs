using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnPlayerFootprints : MonoBehaviour, ICacheRequester
{
    [BoxGroup("References")][Required][SerializeField] private GameObject footprintPrefab;
    
    [SerializeField] private float stepInterval = 0.5f;
    
    private float lastStepTime;
    Transform cacheParent;

    void Update()
    {
        if (!(Time.time - lastStepTime >= stepInterval)){
            return;
        }
        SpawnFootprint();
        lastStepTime = Time.time;
    }

    void SpawnFootprint()
    {
        Instantiate(footprintPrefab, transform.position, transform.rotation, cacheParent);
    }

    public void SetCacheParent(Transform parent)
    {
        cacheParent = parent;
    }
}
