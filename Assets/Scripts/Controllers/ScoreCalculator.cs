using System;
using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;
    public event EventHandler<OnScoreAddedArgs> OnScoreAdded;
    public class OnScoreAddedArgs : EventArgs{
        public int scoreArg;
    }
    private void Awake() {
        UIController.Instance.OnGameRestart += UIController_OnGameRestart;
    }

    private void UIController_OnGameRestart(object sender, EventArgs e) {
        ResetScore();
    }

    public void IncrementScore() {
        score++;
        OnScoreAdded?.Invoke(this, new OnScoreAddedArgs {
            scoreArg = score
        });
    }
    private void ResetScore() {
        score = 0;
        OnScoreAdded?.Invoke(this, new OnScoreAddedArgs {
            scoreArg = score
        });
    }
    public int GetScore() {
        return score;
    }
}