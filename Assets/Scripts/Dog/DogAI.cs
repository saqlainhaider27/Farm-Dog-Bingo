using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : Singleton<DogAI> {

    private Camera mainCamera;
    [SerializeField] private Transform dogSpawn;
    [SerializeField] private LayerMask groundLayer;
    private GameInput gameInput;
    private NavMeshAgent agent;
    private bool gameEnded;

    public event EventHandler<OnBarkEventArgs> OnBark;
    public class OnBarkEventArgs : EventArgs {
        public Vector3 position;
    }
    public event EventHandler<OnDogRunEventArgs> OnDogRun;

    public class OnDogRunEventArgs : EventArgs {
        public Vector3 position;
    }

    private enum DogStates {
        Idle,
        Running,
    }

    private DogStates currentState;
    private float maxStoppingDistance = 0.5f;

    // Delay between event invocations
    private float eventCooldown = 0.5f; // Event can be invoked every 0.2 seconds
    private float lastEventTime = 0f;
    private bool barked = false;
    private float lastBarkTime = 0f;
    private float barkCooldown = 5f;

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

                // Check if enough time has passed to fire the event
                if (Time.time - lastEventTime >= eventCooldown) {
                    lastEventTime = Time.time;
                    OnDogRun?.Invoke(this, new OnDogRunEventArgs {
                        position = transform.position,
                    });
                }
            }
        }
        else {
            agent.isStopped = true;
            currentState = DogStates.Idle;
        }

        if (Time.time - lastBarkTime >= barkCooldown) {
            lastBarkTime = Time.time;
            barked = false;
        }
    }

    private void MoveDogToMousePosition() {
        Ray ray = mainCamera.ScreenPointToRay(gameInput.GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer)) {
            // Agent is running
            agent.SetDestination(hit.point);
        }
    }
    public void Bark() {
        if (!barked) {
            OnBark?.Invoke(this, new OnBarkEventArgs {
                position = transform.position
            });
            barked = true;
        }

    }

    public bool IsRunning() {
        return currentState == DogStates.Running;
    }
}
