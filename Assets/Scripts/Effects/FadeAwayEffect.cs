using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class FadeAwayEffect : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] float startFade = 1f;
    [SerializeField] float fadeDuration = 1f;

    float startTime;
    
    void Start()
    {
        startTime = Time.time;
        var color = spriteRenderer.color;
        color.a = startFade;
        spriteRenderer.color = color;
    }
    
    void Update()
    {
        float elapsed = Time.time - startTime;
        float t = Mathf.Clamp01(elapsed / fadeDuration);
        var color = spriteRenderer.color;
        color.a = Mathf.Lerp(startFade, 0f, t);
        spriteRenderer.color = color;
        if (t >= 1f){
            Destroy(gameObject);
        }
    }
}
