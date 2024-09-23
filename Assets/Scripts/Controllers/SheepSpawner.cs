using System;
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
    private int currentOnSceneCount;

    private float timeToNextSpawn;

    private void Awake() {
        ScoreCalculator.Instance.OnSheepDestroyed += ScoreCalculator_OnSheepDestroyed;
    }

    private void ScoreCalculator_OnSheepDestroyed(object sender, EventArgs e) {
        DecrementOnSceneCount();
    }

    private void Update() {
        if (CheckCanSpawn() && Time.time >= timeToNextSpawn) {
            if (currentOnSceneCount <= maxSpawnCount) {
                SpawnSheep();
            }
            timeToNextSpawn = Time.time + spawnDelay; // Reset the timer
        }
    }

    private bool CheckCanSpawn() {
        return true; 
    }

    private void SpawnSheep() {
        int spawnCount = UnityEngine.Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++) {
            Vector3 spawnLocationVector = GetRandomSpawnLocation();

            // Only spawn if the location is valid
            if (IsSpawnLocationValid(spawnLocationVector)) {
                spawnLocation.position = spawnLocationVector;
                Transform spawnTransform = Instantiate(sheepPrefab, spawnLocation);
                IncrementOnSceneCount();
                spawnTransform.parent = null; // Detach from the spawner
            }
        }
    }

    private void IncrementOnSceneCount() {
        currentOnSceneCount++;
    }
    private void DecrementOnSceneCount() {
        currentOnSceneCount--;
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
