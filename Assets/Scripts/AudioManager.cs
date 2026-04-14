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
    AntLevel,
    BeetleLevel,
    SpiderLevel
    
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

    public static void PlaySFX(SoundType sound)
    {
        instance.sfxSource.PlayOneShot(instance.sfxList[(int)sound]);
    }

    public static void PlayMusic(MusicType music)
    {
        instance.musicSource.PlayOneShot(instance.musicList[(int)music]);
        instance.musicSource.loop = true;
    }

    public static void StopMusic()
    {
        instance.musicSource.Stop();
    }
}
