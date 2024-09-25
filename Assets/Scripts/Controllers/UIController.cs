using System;
using UnityEngine;

public class UIController : Singleton<UIController> {

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject pauseMenu;
    public event EventHandler OnGameStart;
    public event EventHandler OnGameHome;

    private void Awake() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false); 
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void PlayButton() {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
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
    }
    public void PauseButton() {
        Time.timeScale = 0;
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);

        
    }
    public void ResumeButton() {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);
        

    }
    public void HomeButton() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        settingMenu.SetActive(false);
        pauseMenu.SetActive(false);

        

    }
}
