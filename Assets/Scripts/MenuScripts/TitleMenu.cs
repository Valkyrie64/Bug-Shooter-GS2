using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.StopMusic();
        AudioManager.PlayMusic(MusicType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:1);
    }

    public void OptionsClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:2);
    }

    public void CreditsClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:3);
    }

    public void ExitClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        Application.Quit();
    }
}
