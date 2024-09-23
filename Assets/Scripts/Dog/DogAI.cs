using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour {

    private Camera mainCamera;
    [SerializeField] private Transform dogSpawn;
    [SerializeField] private LayerMask groundLayer;
    private GameInput gameInput;
    private NavMeshAgent agent;
    private bool gameEnded;
    private enum DogStates {
    
        Idle,
        Running,
    }
    private DogStates currentState;
    private float maxStoppingDistance = 0.5f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        gameInput = GameInput.Instance;
        mainCamera = Camera.main;

        gameEnded = false;
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
        UIController.Instance.OnGameRestart += OnGameRestart_OnGameRestart;
        
    }

    private void OnGameRestart_OnGameRestart(object sender, System.EventArgs e) {
        agent.isStopped = false;
        gameEnded = false;
        transform.position = dogSpawn.position;
    }

    private void GameTimer_OnGameEnded(object sender, System.EventArgs e) {
        gameEnded = true;
    }

    private void Update() {
        if (!gameEnded) {
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
        else {
            agent.isStopped = true;
            currentState = DogStates.Idle;
        }

    }

    private void MoveDogToMousePosition() {
        Ray ray = mainCamera.ScreenPointToRay(gameInput.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer)) {
            // Agent is running
            agent.SetDestination(hit.point);
        }
    }
    public bool IsRunning() {
        return currentState == DogStates.Running;
    }
}
