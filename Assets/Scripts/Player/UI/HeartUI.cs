using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class HeartUI : MonoBehaviour
    {
        [GetComponent][SerializeField] AnimationController animationController;

        void OnObjectPoolDeactivate()
        {
            animationController.SetAnimation();
        }
        
        void OnFactoryRemove()
        {
            animationController.SetAnimation();
            Destroy(gameObject, animationController.GetAnimation().GetCycleDuration());
        }
    }