using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class HeartUI : MonoBehaviour
    {
        [GetComponent][SerializeField] AnimationController animationController;
        [BoxGroup("References")] [Required] [SerializeField] Animation mergeAnimation;
        [BoxGroup("References")][Required][SerializeField] Animation breakAnimation;

        void OnObjectPoolDeactivate()
        {
            animationController.SetAnimation(breakAnimation);
        }

        void OnFactoryAdd()
        {
            animationController.SetAnimation(mergeAnimation);
        }
        
        void OnFactoryRemove()
        {
            animationController.SetAnimation(breakAnimation);
            Destroy(gameObject, animationController.GetAnimation().GetCycleDuration());
        }
    }