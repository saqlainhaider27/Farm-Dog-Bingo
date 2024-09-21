using UnityEngine;

public class ScoreCalculator : Singleton<ScoreCalculator> {

    private int score;

    public void IncrementScore() {
        score++;
    }
    public int GetScore() {
        return score;
    }
}