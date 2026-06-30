using UnityEngine;
using UnityEngine.InputSystem;
using PinePie.SimpleJoystick;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string rotateObject = "RotateObject";

    [Header("Mobile Joystick")]
    [SerializeField] private JoystickController joystick;

    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction rotateObjectAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 RotationInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool RotateObjectTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        jumpAction = mapReference.FindAction(jump);
        sprintAction = mapReference.FindAction(sprint);
        rotateObjectAction = mapReference.FindAction(rotateObject);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        // Keyboard
        movementAction.performed += ctx =>
        {
            if (joystick == null)
                MovementInput = ctx.ReadValue<Vector2>();
        };

        movementAction.canceled += ctx =>
        {
            if (joystick == null)
                MovementInput = Vector2.zero;
        };

        // Mouse Look
        rotationAction.performed += ctx =>
            RotationInput = ctx.ReadValue<Vector2>();

        rotationAction.canceled += ctx =>
            RotationInput = Vector2.zero;

        // Jump
        jumpAction.performed += ctx =>
            JumpTriggered = true;

        jumpAction.canceled += ctx =>
            JumpTriggered = false;

        // Sprint
        sprintAction.performed += ctx =>
            SprintTriggered = true;

        sprintAction.canceled += ctx =>
            SprintTriggered = false;

        // Rotate Object
        rotateObjectAction.performed += ctx =>
            RotateObjectTriggered = true;

        rotateObjectAction.canceled += ctx =>
            RotateObjectTriggered = false;
    }

private void Update()
{
    if (joystick != null)
    {
        MovementInput = joystick.InputDirection;

        // Matikan mouse look ketika memakai joystick
        RotationInput = Vector2.zero;
    }
}

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}