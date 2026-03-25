using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    [Header("Categories")]
    public GameObject accessabilityOptions;
    public GameObject soundOptions;
    public GameObject controlOptions;
    public GameObject languageOptions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
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
    }

    public void SoundClicked()
    {
        soundOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        controlOptions.SetActive(false);
        languageOptions.SetActive(false);
    }

    public void ControlClicked()
    {
        controlOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        languageOptions.SetActive(false);
    }

    public void LanguageClicked()
    {
        languageOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
    }

    public void QuitClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex:0);
    }
}
