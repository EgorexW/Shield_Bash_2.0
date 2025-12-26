using Sirenix.OdinInspector;
using UnityEngine;

class CharacterMovement : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Rigidbody2D rb;
    
    [SerializeField] float speed = 5f;
    
    Vector2 movement;

    public void SetMovementInput(Vector2 inputVector)
    {
        movement = inputVector.normalized;
    }

    public void SetTarget(Vector2 lookPoint)
    {
        var lookDir = lookPoint - (Vector2)transform.position;
        // lookDir.Normalize();
        rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDir);
    }
    
    void Update()
    {
        Vector2 move = movement * (speed * Time.deltaTime);
        rb.MovePosition(rb.position + move);
    }
}