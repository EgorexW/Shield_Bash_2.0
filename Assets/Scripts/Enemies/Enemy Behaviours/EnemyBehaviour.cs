using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public EnemyBehaviours enemyBehaviours;
    protected bool behaviourActive;
    [SerializeField] float cooldown = 1;
    [SerializeField] bool canRepeat;
    
    public float Cooldown => cooldown;
    public bool CanRepeat => canRepeat;

    [FoldoutGroup("Events")]
    public UnityEvent onStartBehaviour;
    [FoldoutGroup("Events")]
    public UnityEvent onEndBehaviour;

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