using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SoundType
{
    PlayerShot,
    StraightShot,
    TrackingShot,
    WaveShot,
    BarrageShot,
    KamikazeShot,
    WallShot,
    CreateShot,
    RoundShot,
    Explosion,
    RankUp,
    RankDown,
    UISelect,
    UIConfirm,
}

public enum MusicType
{
    Title,
    UIMenu,
    Level1Music,
    Level2Music,
    BossMusic
    
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfxList;
    [SerializeField] private AudioClip[] musicList;
    private static AudioManager instance;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;

    private void Awake()
    {
        if (instance != null)
        {
            var audios =  GameObject.FindGameObjectsWithTag("AudioManager");
            Destroy(audios[0]);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
    }

    void Update()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public static void PlaySFX(SoundType sound)
    {
        instance.sfxSource.PlayOneShot(instance.sfxList[(int)sound]);
    }

    public static void PlayMusic(MusicType music)
    {
        instance.musicSource.clip = instance.musicList[(int)music];
        instance.musicSource.Play();
    }

    public static void StopMusic()
    {
        instance.musicSource.Stop();
    }
}
