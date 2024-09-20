using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour {

    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private GameInput gameInput;
    private NavMeshAgent agent;

    private enum DogStates {
    
        Idle,
        Running,
    }
    private DogStates currentState;
    [SerializeField] private float maxStoppingDistance = 0.5f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        gameInput = GameInput.Instance;
        mainCamera = Camera.main;
    }

    private void Update() {
        MoveDogToMousePosition();
        if (agent.remainingDistance <= maxStoppingDistance) {
            agent.isStopped = true;
            currentState = DogStates.Idle;
        }
        else {
            agent.isStopped = false;
            currentState = DogStates.Running;
        }
    }

    private void MoveDogToMousePosition() {
        Ray ray = mainCamera.ScreenPointToRay(gameInput.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            // Agent is running
            agent.SetDestination(hit.point);
        }
    }
    public bool IsRunning() {
        return currentState == DogStates.Running;
    }
}
