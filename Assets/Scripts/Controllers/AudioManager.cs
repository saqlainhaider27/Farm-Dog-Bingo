using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    [SerializeField] private AudioSource scoreAS;

    private void Start() {
        DogAI.Instance.OnDogRun += DogAI_OnDogRun;
        DogAI.Instance.OnBark += DogAI_OnBark;

        UIController.Instance.OnGameStart += UIController_OnGameStart;
        ScoreCalculator.Instance.OnScoreAdded += ScoreCalculator_OnScoreAdded;
    }

    private void ScoreCalculator_OnScoreAdded(object sender, ScoreCalculator.OnScoreAddedArgs e) {

        scoreAS.Play();
    }

    private void UIController_OnGameStart(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.roosterCry, transform.position);
    }

    private void DogAI_OnBark(object sender, DogAI.OnBarkEventArgs e) {
        PlaySound(audioClipRefsSO.bark, e.position);
    }

    public void PlayWalkSound(Vector3 postion) {
        PlaySound(audioClipRefsSO.walk,postion);
    }
    public void PlayRunSound(Vector3 postion) {
        PlaySound(audioClipRefsSO.gallopSheep, postion);
    }
    public void PlayMooSound(Vector3 postion) {
        PlaySound(audioClipRefsSO.moo, postion);
    }
    public void PlayEatSound(Vector3 postion) {
        PlaySound(audioClipRefsSO.eat, postion);
    }
    private void DogAI_OnDogRun(object sender, DogAI.OnDogRunEventArgs e) {
        PlaySound(audioClipRefsSO.gallop, e.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip , Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}