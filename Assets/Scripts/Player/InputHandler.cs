using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    [GetComponent][SerializeField] PlayerInput playerInput;
    
    [BoxGroup("References")][Required][SerializeField] CharacterMovement characterMovement;
    
    private void Awake()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    void OnActionTriggered(InputAction.CallbackContext obj)
    {
        switch (obj.action.name){
            case "Move":
                OnMovePerformed(obj);
                break;
            default:
                Debug.LogWarning("Unhandled action: " + obj.action.name);
                break;
        }
    }

    void OnMovePerformed(InputAction.CallbackContext obj)
    {
        Vector2 inputVector = obj.ReadValue<Vector2>();
        characterMovement.SetMovementInput(inputVector);
    }
}