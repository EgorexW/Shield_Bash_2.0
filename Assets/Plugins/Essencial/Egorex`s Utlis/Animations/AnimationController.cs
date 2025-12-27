using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animation[] animations;
    [SerializeField] bool startAnimationOnAwake = true;
    [SerializeField] UnityEvent onNewFrame;
    Animation activeAnimation;

    [HideIf("@image != null")][BoxGroup("References")][Required][SerializeField] protected SpriteRenderer spriteRenderer;
    [HideIf("@spriteRenderer != null")] [BoxGroup("References")][Required][SerializeField] Image image;

    public Sprite GetSprite(){
        if (spriteRenderer != null){
            return spriteRenderer.sprite;
        }
        if (image != null){
            return image.sprite;
        }
        throw new Exception("No SpriteRenderer or Image assigned");
    }
    
    public void SetSprite(Sprite sprite){
        if (spriteRenderer != null){
            spriteRenderer.sprite = sprite;
            return;
        }
        if (image != null){
            image.sprite = sprite;
            return;
        }
        throw new Exception("No SpriteRenderer or Image assigned");
    }
    
    void Awake()
    {
        if (startAnimationOnAwake){
            activeAnimation = animations[0];
        }
    }

    void Update()
    {
        if (activeAnimation == null){
            return;
        }
        var prevSprite = GetSprite();
        var newSprite = activeAnimation.GetNextFrame(Time.deltaTime);
        if (prevSprite != newSprite){
            onNewFrame.Invoke();
        }
        SetSprite(newSprite);
    }


    public void SetAnimation(string animationName = null)
    {
        if (activeAnimation != null && activeAnimation.name == animationName){
            return;
        }
        var animation = Array.Find(animations, x => x.name == animationName);
        if (animation == null){
            // Debug.LogWarning("Animation" + animationName + "does not exist");
            // return;
            animation = animations[0];
        }
        activeAnimation = animation;
        activeAnimation.Restart();
    }

    public Animation GetAnimation()
    {
        return activeAnimation;
    }

    public Animation GetSetAnimation(string animationName)
    {
        SetAnimation(animationName);
        return GetAnimation();
    }

    public void StopAnimation()
    {
        activeAnimation = null;
    }
}