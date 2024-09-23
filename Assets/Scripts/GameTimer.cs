using UnityEngine;
using System;

public class GameTimer : Singleton<GameTimer> {
    
    [SerializeField] private float maxPlayTime;
    public event EventHandler OnGameEnded;
    private void Awake() {

        UIController.Instance.OnGameRestart += OnGameRestart_OnGameRestart;
    }

    private void OnGameRestart_OnGameRestart(object sender, EventArgs e) {
        maxPlayTime += Time.time;
    }

    private void Update() {
        if (Time.time >= maxPlayTime){

            OnGameEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
