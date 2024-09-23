using System;
using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;
    public event EventHandler<OnScoreAddedArgs> OnScoreAdded;
    public class OnScoreAddedArgs : EventArgs{
        public int scoreArg;
    }

    public void IncrementScore() {
        score++;
        OnScoreAdded?.Invoke(this, new OnScoreAddedArgs {
            scoreArg = score
        });
    }
    public int GetScore() {
        return score;
    }
}