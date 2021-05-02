using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Movement delegates and events
    public delegate void PlayerMovementPerformed(Vector2 direction);
    public static event PlayerMovementPerformed OnPlayerMovementPerformed;

    public delegate void PlayerMovementCanceled();
    public static event PlayerMovementCanceled OnPlayerMovementCanceled;

    private PlayerInput inputMaster;

    private void Awake()
    {
        inputMaster = new PlayerInput();

        inputMaster.Player.Movement.performed += context => { if (OnPlayerMovementPerformed != null) OnPlayerMovementPerformed(context.ReadValue<Vector2>()); };
        inputMaster.Player.Movement.canceled += context => { if (OnPlayerMovementCanceled != null) OnPlayerMovementCanceled(); };
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}