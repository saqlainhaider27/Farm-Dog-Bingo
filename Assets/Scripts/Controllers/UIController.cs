using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Sliders")]
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;
    private bool paused;
    private bool gameStarted;
    public event EventHandler OnGameStart;
    public event EventHandler<OnMusicVolumeChangedEventArgs> OnMusicVolumeChanged;
    public class OnMusicVolumeChangedEventArgs : EventArgs {
        public float volume;    
    }

    public event EventHandler<OnSFXVolumeChangedEventArgs> OnSFXVolumeChanged;
    public class OnSFXVolumeChangedEventArgs : EventArgs {
        public float volume;
    }

    //public event EventHandler OnGameHome;

    private void Awake() {
        StartGameMenuPreset();


        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
        AudioManager.Instance.OnMusicVolumeChanged += AudioManager_OnMusicVolumeChanged;
        AudioManager.Instance.OnSFXVolumeChanged += AudioManager_OnSFXVolumeChanged;
        GameInput.Instance.OnEscapePressed += GameInput_OnEscapePressed;
    }

    private void GameInput_OnEscapePressed(object sender, EventArgs e) {
        TogglePause();
    }

    private void AudioManager_OnSFXVolumeChanged(object sender, AudioManager.OnSFXVolumeChangedEventArgs e) {
        SFXSlider.value = e.volume;
    }

    private void AudioManager_OnMusicVolumeChanged(object sender, AudioManager.OnMusicVolumeChangedEventArgs e) {
        musicSlider.value = e.volume;
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {

        gameStarted = false;
        StartGameMenuPreset();
    }
    public void StartGameMenuPreset() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(false);

        scoreText.text = ScoreCalculator.Instance.GetScore().ToString();
        highScoreText.text = ScoreCalculator.Instance.GetHighScore().ToString();
    }

    public void PlayButton() {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        scoreCanvas.SetActive(true);
        OnGameStart?.Invoke(this, EventArgs.Empty);
        gameStarted = true;
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
        
        gameStarted = false;
    }
    public void BackButton() {
        if (GameTimer.Instance.GetTimeSinceGameStart() == 0) {
            HomeButton();
        }
        else {
            PauseButton();
        }


    }

    public void SetMusicSliderValue() {
        float _volume = musicSlider.value;
        OnMusicVolumeChanged?.Invoke(this, new OnMusicVolumeChangedEventArgs {
            volume = _volume
        });
    }
    public void SetSFXSliderValue() {
        float _volume = SFXSlider.value;

        OnSFXVolumeChanged?.Invoke(this, new OnSFXVolumeChangedEventArgs {
            volume = _volume
        });
    }
    public void TogglePause() {
        if (gameStarted) {
            if (paused) {
                ResumeButton();
            }
            else {
                PauseButton();
            }
            paused = !paused;
        }

    }

}
