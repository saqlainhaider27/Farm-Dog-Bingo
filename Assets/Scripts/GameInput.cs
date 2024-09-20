using UnityEngine;

public class GameInput : Singleton<GameInput> {

    private InputActions inputActions;
    private void Awake() {
        inputActions = new InputActions();
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
