using UnityEngine;

public class DogAnimator : MonoBehaviour {

    [SerializeField] private DogAI dogAI;
    private Animator animator;
    private const string RUNNING = "Running";
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        animator.SetBool(RUNNING, dogAI.IsRunning());
    }
}