using System;
using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;
    public event EventHandler<OnScoreAddedArgs> OnScoreAdded;
    public class OnScoreAddedArgs : EventArgs{
        public int scoreArg;
    }
    public event EventHandler<OnScoreResetEventArgs> OnScoreReset;
    public class OnScoreResetEventArgs : EventArgs {
        public int scoreArg;
    }
    private void Awake() {
        UIController.Instance.OnGameStart += UIController_OnGameStart;
    }

    private void UIController_OnGameStart(object sender, EventArgs e) {
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
        OnScoreReset?.Invoke(this, new OnScoreResetEventArgs {
            scoreArg = score
        });
    }
    public int GetScore() {
        return score;
    }
}