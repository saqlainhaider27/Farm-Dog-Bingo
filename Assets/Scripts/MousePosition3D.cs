using UnityEngine;

public class MousePosition3D : Singleton<MousePosition3D> {

    private GameInput gameInput;
    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private Vector3 movePosition;
    private void Awake() {
        mainCamera = Camera.main;
        gameInput = GameInput.Instance;
    }
    private void Update() {
        Ray ray = mainCamera.ScreenPointToRay(gameInput.GetMousePosition());
        if (Physics.Raycast(ray,out RaycastHit hit, float.MaxValue, layerMask)) {
            movePosition = hit.point;
        }
    }
    public Vector3 GetMovePosition() {
        return movePosition;
    }
}
