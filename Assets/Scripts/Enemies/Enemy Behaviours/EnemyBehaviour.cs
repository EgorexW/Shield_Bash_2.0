using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public EnemyBehaviours enemyBehaviours;
    [SerializeField] float cooldown = 1;
    [SerializeField] bool canRepeat;

    [FoldoutGroup("Events")] public UnityEvent onStartBehaviour;

    [FoldoutGroup("Events")] public UnityEvent onEndBehaviour;

    protected bool behaviourActive;

    public float Cooldown => cooldown;
    public bool CanRepeat => canRepeat;

    public virtual void StartBehaviour()
    {
        behaviourActive = true;
        onStartBehaviour.Invoke();
    }

    public virtual void EndBehaviour()
    {
        behaviourActive = false;
        onEndBehaviour.Invoke();
    }
}