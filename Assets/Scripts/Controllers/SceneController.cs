using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private void Awake() {
        UIController.Instance.OnGameHome += UIController_OnGameHome;
    }

    private void UIController_OnGameHome(object sender, EventArgs e) {
        //ResetScene();
    }

    private void ResetScene() {
        // Get the current scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

}