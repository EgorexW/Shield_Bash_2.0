using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] bool removeAfterTrigger;

    public UnityEvent<Player> onPlayerEntered;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null){
            return;
        }
        // Debug.Log("Player entered the trigger area.", this);
        onPlayerEntered.Invoke(player);
        if (removeAfterTrigger){
            Destroy(gameObject);
        }
    }

    void OnValidate()
    {
        var collider = GetComponent<Collider2D>();
        if (collider.isTrigger){
            return;
        }
        collider.isTrigger = true;
        Debug.Log("Collider changed to trigger", this);
    }
}