using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    [GetComponent] [SerializeField] PlayerInput playerInput;

    [BoxGroup("References")] [Required] [SerializeField] CharacterMovement characterMovement;
    [BoxGroup("References")] [Required] [SerializeField] Shield shieldInput;

    void Awake()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    void OnActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.actionMap.name != "Player"){
            return;
        }
        switch (obj.action.name){
            case "Move":
                OnMovePerformed(obj);
                break;
            case "Target":
                OnTargetPerformed(obj);
                break;
            case "Shield":
                OnShootPerformed(obj);
                break;
            default:
                Debug.LogWarning("Unhandled action: " + obj.action.name);
                break;
        }
    }

    void OnShootPerformed(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValue<float>();
        // Debug.Log("Shield input: " + isPressed);
        if (isPressed > 0.5f){
            shieldInput.Raise();
        }
        else{
            shieldInput.Release();
        }
    }

    void OnTargetPerformed(InputAction.CallbackContext obj)
    {
        var lookPoint = obj.ReadValue<Vector2>();
        lookPoint = General.GetMouseWorldPos(lookPoint);
        characterMovement.SetTarget(lookPoint);
    }

    void OnMovePerformed(InputAction.CallbackContext obj)
    {
        var inputVector = obj.ReadValue<Vector2>();
        characterMovement.SetMovementInput(inputVector);
    }
}