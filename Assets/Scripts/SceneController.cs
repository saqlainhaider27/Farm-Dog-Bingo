using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private void Awake() {
        UIController.Instance.OnGameRestart += UIController_OnGameRestart;
    }

    private void UIController_OnGameRestart(object sender, EventArgs e) {
        //ResetScene();
    }

    private void ResetScene() {
        // Get the current scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

}