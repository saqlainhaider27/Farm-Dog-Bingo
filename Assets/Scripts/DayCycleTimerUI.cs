using UnityEngine;
using UnityEngine.UI;

public class DayCycleTimerUI : MonoBehaviour {

    private Image fillImage;

    private void Awake() {
        fillImage = GetComponent<Image>();
    }

    private void Update() {
        float targetFillAmount = GameTimer.Instance.GetTimeSinceGameStart(); // Get the current progress.
        fillImage.fillAmount = targetFillAmount;
    }
}
