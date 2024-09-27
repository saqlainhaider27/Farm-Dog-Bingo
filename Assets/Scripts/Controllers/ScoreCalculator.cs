using System;
using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;
    private int highScore;
    private string HIGH_SCORE = "HighScore";

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
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
        SetHighScore();
    }

    private void SetHighScore() {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE);
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {
        PlayerPrefs.SetInt(HIGH_SCORE, highScore);
    }

    private void UIController_OnGameStart(object sender, EventArgs e) {
        ResetScore();
    }

    public void IncrementScore() {
        score++;
        if (score > highScore) {
            highScore = score;
        }
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
    public int GetHighScore() {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
}