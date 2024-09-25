using System;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController> {

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject pauseMenu;
    [Header("Canvas")]
    [SerializeField] private GameObject scoreCanvas;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public event EventHandler OnGameStart;
    //public event EventHandler OnGameHome;

    private void Awake() {
        StartGame();
        scoreText.text = ScoreCalculator.Instance.GetScore().ToString();
        highScoreText.text = ScoreCalculator.Instance.GetHighScore().ToString();
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {

        scoreText.text = ScoreCalculator.Instance.GetScore().ToString();
        highScoreText.text = ScoreCalculator.Instance.GetHighScore().ToString();

        StartGame();
    }
    public void StartGame() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(false);
    }

    public void PlayButton() {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(true);
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    public void QuitButton() {
#if UNITY_EDITOR
        // Stop playing the scene in the editor.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application.
        Application.Quit();
#endif
    }
    public void SettingsButton() {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(true);
    }
    public void PauseButton() {
        Time.timeScale = 0;
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);
        scoreCanvas.SetActive(true);

    }
    public void ResumeButton() {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(true);

    }
    public void HomeButton() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(false);
        

    }
}
