using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController playerController;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;

    InputSystem_Actions controls;
    Vector2 moveDirection;
    float speed;
    bool isSprinting = false;

    [SerializeField] Animator playerAnimator;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => OnMove(ctx.ReadValue<Vector2>());

        controls.Player.Sprint.performed += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;
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
        speed = isSprinting ? sprintSpeed : movementSpeed;

        // move player in the direction of the keyboard input    
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y);
        playerController.Move(move * speed * Time.deltaTime);

        // trigger animation in the animator
        float speedFromInput = move.magnitude;
        playerAnimator.SetFloat("speed", speedFromInput);

        // rotate player to face the move direction
        if (move != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(move);
        }
    }
}
