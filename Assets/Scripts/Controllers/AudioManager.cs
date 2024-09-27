using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    [Header("AudioRefs")]
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [Header("AudioSource Refs")]
    [SerializeField] private AudioSource scoreAS;
    [SerializeField] private AudioSource musicAS;
   

    private const string MUSIC_VOLUME_PREF = "MusicVolume";
    private const string SFX_VOLUME_PREF = "SFXVolume";

    private float sfxVolume;


    public event EventHandler<OnMusicVolumeChangedEventArgs> OnMusicVolumeChanged;

    public event EventHandler<OnSFXVolumeChangedEventArgs> OnSFXVolumeChanged;
    public class OnMusicVolumeChangedEventArgs : EventArgs {
        public float volume;
    }
    public class OnSFXVolumeChangedEventArgs : EventArgs {
        public float volume;
    }
    private void Awake() {
        StartGameVolumeSettings();
    }

    private void Start() {
        DogAI.Instance.OnDogRun += DogAI_OnDogRun;
        DogAI.Instance.OnBark += DogAI_OnBark;

        UIController.Instance.OnGameStart += UIController_OnGameStart;
        ScoreCalculator.Instance.OnScoreAdded += ScoreCalculator_OnScoreAdded;

        UIController.Instance.OnMusicVolumeChanged += UIController_OnMusicVolumeChanged;
        UIController.Instance.OnSFXVolumeChanged += UIController_OnSFXVolumeChanged;
    }

    private void UIController_OnSFXVolumeChanged(object sender, UIController.OnSFXVolumeChangedEventArgs e) {
        SetSFXVolume(e.volume);
    }

    private void UIController_OnMusicVolumeChanged(object sender, UIController.OnMusicVolumeChangedEventArgs e) {
        SetMusicVolume(e.volume);
    }

    private void ScoreCalculator_OnScoreAdded(object sender, ScoreCalculator.OnScoreAddedArgs e) {

        scoreAS.Play();
    }

    private void UIController_OnGameStart(object sender, System.EventArgs e) {

        StartGameVolumeSettings();
        PlaySound(audioClipRefsSO.roosterCry, transform.position);
    }
    public void StartGameVolumeSettings() {
        if (PlayerPrefs.HasKey(MUSIC_VOLUME_PREF)) {
            float _volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF);
            OnMusicVolumeChanged?.Invoke(this, new OnMusicVolumeChangedEventArgs {
                volume = _volume
            });
            musicAS.volume = _volume;
        }
        else {
            SetDefaultVolumeSetting();
        }
        if (PlayerPrefs.HasKey(SFX_VOLUME_PREF)) {
            float _volume = PlayerPrefs.GetFloat(SFX_VOLUME_PREF);
            OnSFXVolumeChanged?.Invoke(this, new OnSFXVolumeChangedEventArgs {
                volume = _volume
            });
            scoreAS.volume = _volume;
            sfxVolume = _volume;
        }
        else {
            SetDefaultVolumeSetting();
        }
    }

    private void SetDefaultVolumeSetting() {
        musicAS.volume = 1;
        sfxVolume = 1;
        scoreAS.volume = 1;
        OnMusicVolumeChanged?.Invoke(this, new OnMusicVolumeChangedEventArgs {
            volume = musicAS.volume,
        });
        OnSFXVolumeChanged?.Invoke(this, new OnSFXVolumeChangedEventArgs {
            volume = sfxVolume,
        });
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
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip , Vector3 position, float volume = 1f) {
        volume = sfxVolume;
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    public void SetMusicVolume(float _volume) {
        musicAS.volume = _volume;
        
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREF, _volume);
        OnMusicVolumeChanged?.Invoke(this, new OnMusicVolumeChangedEventArgs {
            volume = _volume
        });

    }
    public void SetSFXVolume(float _volume) {
        scoreAS.volume = _volume;
        sfxVolume = _volume;

        PlayerPrefs.SetFloat(SFX_VOLUME_PREF, _volume);
        OnSFXVolumeChanged?.Invoke(this, new OnSFXVolumeChangedEventArgs {
            volume = _volume
        });

    }
}