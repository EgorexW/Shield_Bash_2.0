using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class SaveTrigger : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Level level;
    
    [SerializeField] SaveTriggerType triggerType;

    [FoldoutGroup("Events")] public UnityEvent whenTriggerActive;
    
    void Start()
    {
        bool triggered = level.levelReference.GetSaveData().triggersData.GetTrigger(triggerType);
        if (triggered)
        {
            whenTriggerActive.Invoke();
        }
    }

    public void UpdateTrigger(bool triggered)
    {
        level.levelReference.GetSaveData().triggersData.SetTrigger(triggerType, triggered);
    }
}

public enum SaveTriggerType
{
    TheKnightDefeated
}