using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelSize : MonoBehaviour
{
    const float LEVEL_WIDTH_TO_SIZE_RATIO = 3.555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555f;
        
    [BoxGroup("References")][GetComponent][SerializeField] SpriteRenderer spriteRenderer;
    [BoxGroup("References")] [Required] [SerializeField] Level level;

    void Awake()
    {
        UpdateSize();
        spriteRenderer.enabled = false;
    }

    [Button]
    void UpdateSize()
    {
        transform.localScale = new Vector3(level.LevelSize * LEVEL_WIDTH_TO_SIZE_RATIO, level.LevelSize * 2, 1);
    }
}
