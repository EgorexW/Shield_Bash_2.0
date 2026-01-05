using Sirenix.OdinInspector;
using UnityEngine;

class CharacterMovement : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Rigidbody2D rb;
    
    [SerializeField] float speed = 5f;
    
    Vector2 movement;
    Vector2 lookPoint;

    public void SetMovementInput(Vector2 inputVector)
    {
        movement = inputVector.normalized;
    }

    public void SetTarget(Vector2 lookPoint)
    {
        this.lookPoint = lookPoint;
    }
    
    void FixedUpdate()
    {
        Vector2 move = movement * (speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + move);
        
        var lookDir = lookPoint - (Vector2)transform.position;
        rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }
}