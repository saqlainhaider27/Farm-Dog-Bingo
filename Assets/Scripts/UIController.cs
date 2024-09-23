using System;
using UnityEngine;

public class UIController : Singleton<UIController> {

    [SerializeField] private GameObject endMenu;

    public event EventHandler OnGameRestart;

    private void Awake() {
        endMenu.SetActive(false);
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {
        endMenu.SetActive(true);
    }

    public void RetryButton() {
        endMenu.SetActive(false);
        OnGameRestart?.Invoke(this, EventArgs.Empty);
    }
}
