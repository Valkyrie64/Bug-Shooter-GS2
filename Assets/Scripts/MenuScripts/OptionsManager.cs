using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    [Header("Categories")]
    public GameObject accessabilityOptions;
    public GameObject soundOptions;
    public GameObject controlOptions;
    public GameObject languageOptions;

    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.StopMusic();
        AudioManager.PlayMusic(MusicType.UIMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccesClicked()
    {
        accessabilityOptions.SetActive(true);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void SoundClicked()
    {
        soundOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
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
}
