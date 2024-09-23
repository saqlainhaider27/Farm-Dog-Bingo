using System;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : Singleton<SheepSpawner> {

    [SerializeField] private Vector3 spawnBoundsMin;
    [SerializeField] private Vector3 spawnBoundsMax;

    [SerializeField] private Vector3 excludeBoundsMin;
    [SerializeField] private Vector3 excludeBoundsMax;

    [SerializeField] private Transform sheepPrefab;
    [SerializeField] private Transform spawnLocation;

    [SerializeField] private int minSpawnCount; // Minimum number of sheep to spawn
    [SerializeField] private int maxSpawnCount; // Maximum number of sheep to spawn

    [SerializeField] private float spawnDelay; // Delay between spawn events

    [SerializeField] private int maxOnSceneCount;

    private List<SheepAI> onSceneSheepAIList = new List<SheepAI>();

    private float timeToNextSpawn;
    private bool canSpawn = true;

    private void Awake() {
        canSpawn = true;

        GameTimer.Instance.OnGameEnded += GameTimer_OnGameEnded;
        UIController.Instance.OnGameRestart += UIController_OnGameRestart;
    }

    private void UIController_OnGameRestart(object sender, EventArgs e) {
        canSpawn = true;
        foreach (SheepAI sheepAI in onSceneSheepAIList) {
            sheepAI.DestroySelf();
        }
    }

    private void GameTimer_OnGameEnded(object sender, EventArgs e) {
        canSpawn = false;
        foreach (SheepAI sheepAI in onSceneSheepAIList) {
            sheepAI.StopSheep();
        }
    }



    private void Update() {
        if (canSpawn && Time.time >= timeToNextSpawn) {
            if (onSceneSheepAIList.Count <= maxSpawnCount) {
                SpawnSheep();
            }
            timeToNextSpawn = Time.time + spawnDelay; // Reset the timer
        }
    }

    private void SpawnSheep() {
        int spawnCount = UnityEngine.Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++) {
            Vector3 spawnLocationVector = GetRandomSpawnLocation();

            // Only spawn if the location is valid
            if (IsSpawnLocationValid(spawnLocationVector)) {
                spawnLocation.position = spawnLocationVector;
                Transform spawnTransform = Instantiate(sheepPrefab, spawnLocation);
                onSceneSheepAIList.Add(spawnTransform.gameObject.GetComponent<SheepAI>());
                spawnTransform.parent = null; // Detach from the spawner
            }
        }
    }
    public void RemoveSheepFromOnSceneList(SheepAI _sheepAI) {
        onSceneSheepAIList.Remove( _sheepAI );
    }

    private Vector3 GetRandomSpawnLocation() {
        float spawnLocationX = UnityEngine.Random.Range(spawnBoundsMin.x, spawnBoundsMax.x);
        float spawnLocationZ = UnityEngine.Random.Range(spawnBoundsMin.z, spawnBoundsMax.z);
        return new Vector3(spawnLocationX, 0f, spawnLocationZ);
    }

    private bool IsSpawnLocationValid(Vector3 location) {
        return location.x < excludeBoundsMin.x || location.x > excludeBoundsMax.x ||
               location.z < excludeBoundsMin.z || location.z > excludeBoundsMax.z;
    }
}
