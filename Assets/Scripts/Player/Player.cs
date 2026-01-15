using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelManager levelManager;
    [BoxGroup("References")] [Required] [SerializeField] Shield shield;
    [BoxGroup("References")] [Required] [SerializeField] CharacterHealth characterHealth;

    public Shield Shield => shield;
    public CharacterHealth CharacterHealth => characterHealth;

    void Awake()
    {
        foreach (var cacheRequester in GetComponentsInChildren<ICacheRequester>()){
            cacheRequester.SetCacheParent(GetCacheParent());
        }
    }

    public void Teleport(Vector3 transformPosition)
    {
        transform.position = transformPosition;
    }

    public Transform GetCacheParent()
    {
        return levelManager.GetPlayerCacheParent();
    }
}