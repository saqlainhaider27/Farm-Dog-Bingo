using System;
using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;
    public event EventHandler<OnScoreAddedArgs> OnScoreAdded;
    public event EventHandler OnSheepDestroyed; 
    public class OnScoreAddedArgs : EventArgs{
        public int scoreArg;
    }

    public void IncrementScore() {
        score++;
        OnScoreAdded?.Invoke(this, new OnScoreAddedArgs {
            scoreArg = score
        });
        OnSheepDestroyed?.Invoke(this, EventArgs.Empty);
    }
    public int GetScore() {
        return score;
    }
}