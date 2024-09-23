using UnityEngine;

public class DayCycleAnimator : MonoBehaviour {

    private bool gameReset;
    private Animator animator;
    private const string GAME_RESET = "GameReset";

    private void Awake() {
        UIController.Instance.OnGameRestart += UIController_OnGameRestart;
        animator = GetComponent<Animator>();
        gameReset = false;
    }

    private void UIController_OnGameRestart(object sender, System.EventArgs e) {
        gameReset = !gameReset;
    }
    private void Update() {
        animator.SetBool(GAME_RESET, gameReset);
    }
}
