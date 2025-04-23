using UnityEngine;

public class MovementController : MonoBehaviour
{
    InputManager inputManager;
    CharacterController characterController;

    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.moveDir.magnitude != 0)
        {
            transform.forward = new Vector3(inputManager.moveDir.x, 0, inputManager.moveDir.y);
            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
        
    }

}
