using UnityEngine;
using System;

public class GameTimer : Singleton<GameTimer> {

    [SerializeField] private float maxPlayTime; // This should be set to the desired play time duration in seconds.
    public event EventHandler OnGameEnded;

    private float timeSinceGameStart;
    private float gameStartTime; // Store the start time of the game.

    private bool gameStarted;

    private void Awake() {
        timeSinceGameStart = 0;
        gameStartTime = Time.time;
        UIController.Instance.OnGameStart += OnGameRestart_OnGameStart;
        OnGameEnded += GameTimer_OnGameEnded;
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {
        gameStarted = false;
        timeSinceGameStart = 0;

    }

    private void OnGameRestart_OnGameStart(object sender, EventArgs e) {
        gameStarted = true;
        timeSinceGameStart = 0;
        gameStartTime = Time.time; // Reset the start time to the current time.
    }

    private void Update() {
        if (gameStarted) {

            timeSinceGameStart = Time.time - gameStartTime; // Calculate elapsed time since the last start/restart.
            if (timeSinceGameStart >= maxPlayTime) {
                OnGameEnded?.Invoke(this, EventArgs.Empty);
            }
            //Debug.Log(timeSinceGameStart);
        }
    }

    public float GetTimeSinceGameStartNormalized() {
        return Mathf.Clamp01(timeSinceGameStart / maxPlayTime); // Ensure the returned value is between 0 and 1.
    }
    public float GetTimeSinceGameStart() {
        return timeSinceGameStart;
    }
}
