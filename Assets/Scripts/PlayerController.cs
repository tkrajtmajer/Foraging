using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController playerController;
    [SerializeField] float movementSpeed = 4f;

    InputSystem_Actions controls;
    Vector2 moveDirection;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => OnMove(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnMove(Vector2 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y);

        playerController.Move(move * movementSpeed * Time.deltaTime);
    }
}
