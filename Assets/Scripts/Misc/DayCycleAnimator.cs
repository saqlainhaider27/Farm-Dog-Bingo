using UnityEngine;

public class DayCycleAnimator : MonoBehaviour {

    private bool gameReset;
    private bool gameStart;

    private Animator animator;
    private const string GAME_RESET = "GameReset";
    private const string GAME_START = "GameStart";

    private void Awake() {
        UIController.Instance.OnGameStart += UIController_OnGameStart;
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
        animator = GetComponent<Animator>();
        gameReset = false;
        gameStart = false;
    }

    private void GameTimer_OnGameEnded(object sender, System.EventArgs e) {
        gameStart = false;
    }

    private void UIController_OnGameStart(object sender, System.EventArgs e) {
        gameReset = !gameReset;
        gameStart = true;
        animator.SetBool(GAME_START, gameStart);
        animator.SetBool(GAME_RESET, gameReset);
    }
}
