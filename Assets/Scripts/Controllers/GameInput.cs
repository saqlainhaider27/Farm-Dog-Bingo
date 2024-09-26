using System;
using UnityEngine;

public class GameInput : Singleton<GameInput> {

    private InputActions inputActions;
    public event EventHandler OnEscapePressed;

    private void Awake() {
        inputActions = new InputActions();

        inputActions.Player.Escape.performed += Escape_performed;
    }

    private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnEscapePressed.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMousePosition() {
        return inputActions.Player.MousePosition.ReadValue<Vector2>();
    }
    private void OnEnable() {
        inputActions.Enable();
    }
    private void OnDisable() {
        inputActions.Disable();
    }
}
