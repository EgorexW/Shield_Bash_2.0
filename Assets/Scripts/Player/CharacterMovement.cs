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
    
    void Update()
    {
        Vector2 move = movement * (speed * Time.deltaTime);
        rb.MovePosition(rb.position + move);
    }
}