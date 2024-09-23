using System;
using UnityEngine;
using UnityEngine.AI;

public class SheepAI : MonoBehaviour {

    private NavMeshAgent agent;

    [Header("Move Distance Settings")]
    [SerializeField] private float minWalkDistance = 5f;
    [SerializeField] private float maxWalkDistance = 15f;
    [SerializeField] private float eatDuration = 3f;

    [Header("Speed Settings")]
    [SerializeField] private float defaultSpeed = 3.5f;
    [SerializeField] private float runSpeed = 7f;

    [Header("Misc")]
    [SerializeField] private float searchRadius = 10f;
    [SerializeField] private LayerMask dogLayer;

    // Define a target coordinate group for the sheep to move towards
    [SerializeField] private Vector3 targetGroup = new Vector3(0, 0, 0);

    private float timeToNextAction;


    private enum SheepStates {
        Idle,
        Walking,
        Running,
        Eating
    }
    private SheepStates currentState = SheepStates.Idle;
    [SerializeField] private LayerMask winPointLayer;
    private bool isDestroyed;
    private bool gameEnded;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = defaultSpeed;
        isDestroyed = false;

        gameEnded = false;

    }


    public void StopSheep() {
        gameEnded = true;
        currentState = SheepStates.Idle;
        agent.isStopped = true;
    }

    private void Update() {
        if (!isDestroyed && !gameEnded) {
            CheckForDogs();

            switch (currentState) {
                case SheepStates.Idle:
                HandleIdleState();
                break;
                case SheepStates.Walking:
                HandleWalkingState();
                break;
                case SheepStates.Running:
                HandleRunningState();
                break;
                case SheepStates.Eating:
                HandleEatingState();
                break;
            }
            int maxDistance = 1;
            bool winPointInFront = Physics.Raycast(transform.position, transform.forward, maxDistance, winPointLayer);
            if (winPointInFront) {
                ScoreCalculator.Instance.IncrementScore();
                DestroySelf();
            }
        }
    }

    private void CheckForDogs() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, dogLayer);
        if (colliders.Length > 0) {
            // If a dog is detected, switch to running state
            currentState = SheepStates.Running;
            agent.speed = runSpeed;

            // Calculate run direction away from the dog
            Vector3 runDirection = Vector3.zero;
            foreach (Collider collider in colliders) {
                Vector3 dogPosition = collider.transform.position;
                Vector3 directionAwayFromDog = (transform.position - dogPosition).normalized;

                // Combine the run direction away from the dog with the target group direction
                Vector3 desiredDirection = (targetGroup - transform.position).normalized;
                runDirection += (directionAwayFromDog + desiredDirection).normalized; // Sum and normalize the directions
            }

            // Set the destination to move in the calculated direction
            float distance = UnityEngine.Random.Range(minWalkDistance, maxWalkDistance);

            agent.SetDestination(transform.position + runDirection * distance);
            

        }
    }

    private void HandleIdleState() {
        // Transition to walking after some time
        if (Time.time >= timeToNextAction) {
            SetRandomDestination();
        }
    }

    private void HandleWalkingState() {
        if (agent.remainingDistance <= agent.stoppingDistance && currentState != SheepStates.Eating) {
            currentState = SheepStates.Eating;
            timeToNextAction = Time.time + eatDuration;
        }
    }

    private void HandleRunningState() {
        if (agent.remainingDistance <= agent.stoppingDistance) {
            currentState = SheepStates.Idle;
            agent.speed = defaultSpeed; // Reset speed when idle
            int waitMin = 2;
            int waitMax = 5;
            timeToNextAction = Time.time + UnityEngine.Random.Range(waitMin, waitMax); // Wait before moving again
        }
    }

    private void HandleEatingState() {
        if (Time.time >= timeToNextAction) {
            currentState = SheepStates.Idle;
            SetRandomDestination();
        }
    }

    private void SetRandomDestination() {
        float distance = UnityEngine.Random.Range(minWalkDistance, maxWalkDistance);
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas);

        agent.SetDestination(hit.position);
        currentState = SheepStates.Walking;
    }
    public void DestroySelf() {
        isDestroyed = true;
        SheepSpawner.Instance.RemoveSheepFromOnSceneList(this);
        Destroy(this.gameObject);
    }
    public bool IsWalking() {
        return currentState == SheepStates.Walking;
    }
    public bool IsEating() {
        return currentState == SheepStates.Eating;
    }
    public bool IsRunning() {
        return currentState == SheepStates.Running;
    }
}
