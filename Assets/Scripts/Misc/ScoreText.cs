using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI textMeshPro;
    private void Awake() {
        ScoreCalculator.Instance.OnScoreAdded += ScoreCalculator_OnScoreAdded;
        ScoreCalculator.Instance.OnScoreReset += ScoreCalculator_OnScoreReset;
    }

    private void ScoreCalculator_OnScoreReset(object sender, ScoreCalculator.OnScoreResetEventArgs e) {
        textMeshPro.text = e.scoreArg.ToString();
    }

    private void ScoreCalculator_OnScoreAdded(object sender, ScoreCalculator.OnScoreAddedArgs e) {
        textMeshPro.text = e.scoreArg.ToString();
    }
}