using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI textMeshPro;
    private void Awake() {
        ScoreCalculator.Instance.OnScoreAdded += ScoreCalculator_OnScoreAdded;
    }

    private void ScoreCalculator_OnScoreAdded(object sender, ScoreCalculator.OnScoreAddedArgs e) {
        textMeshPro.text = e.scoreArg.ToString();
    }
}