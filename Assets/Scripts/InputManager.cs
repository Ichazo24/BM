using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputSystem_Actions actions;
    

    public Vector2 moveDir;

    [HideInInspector] public UnityEvent OnBombPressed; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        actions = new InputSystem_Actions();
        actions.Enable();
        actions.Player.Move.performed += i => moveDir = i.ReadValue<Vector2>();
        actions.Player.Move.canceled += i => moveDir = Vector2.zero;
    }

    // Update is called once per frame
    

}
