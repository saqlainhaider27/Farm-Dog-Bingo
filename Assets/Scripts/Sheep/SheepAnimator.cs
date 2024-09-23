using UnityEngine;

public class SheepAnimator : MonoBehaviour {

    private const string RUNNING = "Running";
    private const string EATING = "Eating" ;
    private const string WALKING = "Walking";
    private const string IDLE = "Idle";

    private Animator animator;
    [SerializeField] private SheepAI sheepAI;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        animator.SetBool (RUNNING, sheepAI.IsRunning());
        animator.SetBool (WALKING, sheepAI.IsWalking());
        animator.SetBool(EATING, sheepAI.IsEating());
    }
}
