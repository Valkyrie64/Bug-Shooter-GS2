using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Categories")]
    public GameObject accessabilityOptions;
    public GameObject soundOptions;
    public GameObject controlOptions;
    public GameObject languageOptions;

    [Header("Accessibility")]
    public Toggle autoFireToggle;
    public Toggle arachnophobiaToggle;

    [Header("Sound Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    
    void Start()
    {
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.StopMusic();
        AudioManager.PlayMusic(MusicType.UIMenu);
    }

    public void AccesClicked()
    {
        accessabilityOptions.SetActive(true);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        
        int currentAutoFire = PlayerPrefs.GetInt("AutoFire");
        if (currentAutoFire == 0)
        {
            autoFireToggle.isOn = false;
        }
        if (currentAutoFire == 1)
        {
            autoFireToggle.isOn = true;
        }
        
        int currentArachnophobia = PlayerPrefs.GetInt("Arachnophobia");
        if (currentArachnophobia == 0)
        {
            arachnophobiaToggle.isOn = false;
        }
        if (currentArachnophobia == 1)
        {
            arachnophobiaToggle.isOn = true;
        }
        
    }

    public void SoundClicked()
    {
        soundOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void ControlClicked()
    {
        controlOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void LanguageClicked()
    {
        languageOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void QuitClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:0);
    }

    public void AutoFireClicked()
    {
        if (autoFireToggle.isOn)
        {
            AudioManager.PlaySFX(SoundType.UIConfirm);
            PlayerPrefs.SetInt("AutoFire", 1);
        }
        if (autoFireToggle.isOn == false)
        {
            AudioManager.PlaySFX(SoundType.UIConfirm);
            PlayerPrefs.SetInt("AutoFire", 0);
        }
    }

    public void ArachnophobiaClicked()
    {
        if (arachnophobiaToggle.isOn)
        {
            AudioManager.PlaySFX(SoundType.UIConfirm);
            PlayerPrefs.SetInt("Arachnophobia", 1);
        }
        if (arachnophobiaToggle.isOn == false)
        {
            AudioManager.PlaySFX(SoundType.UIConfirm);
            PlayerPrefs.SetInt("Arachnophobia", 0);
        }
    }

    public void MusicSliderMoved()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXSliderMoved()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
