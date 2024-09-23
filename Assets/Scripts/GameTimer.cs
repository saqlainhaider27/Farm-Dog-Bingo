using UnityEngine;

public class GameTimer : MonoBehaviour {
    
    [SerializeField] private SheepSpawner spawner;
    [SerializeField] private DogAI dog;

    [SerializeField] private float maxPlayTime;

    private void Update() {
        if (Time.time >= maxPlayTime){
            spawner.enabled = false;
            dog.enabled = false;
            
        }
    }
}
