using UnityEngine;

public class DayCycleAnimator : MonoBehaviour {

    private bool gameReset;
    private Animator animator;
    private const string GAME_RESET = "GameReset";

    private void Awake() {
        UIController.Instance.OnGameStart += UIController_OnGameStart;
        animator = GetComponent<Animator>();
        gameReset = false;
    }

    private void UIController_OnGameStart(object sender, System.EventArgs e) {
        gameReset = !gameReset;

        animator.SetBool(GAME_RESET, gameReset);
    }
}
