using System.Collections.Concurrent;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Categories")]
    public GameObject accessabilityOptions;
    public GameObject soundOptions;
    public GameObject languageOptions;

    [Header("Accessibility")]
    public Toggle autoFireToggle;
    public Toggle arachnophobiaToggle;

    [Header("Sound Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    
    [Header("UI Elements")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private InputSystemUIInputModule inputModule;
    [SerializeField] private Selectable[] firstItems;
    private bool insideOptions;
    private PlayerInputActions uiActions;


    void Awake()
    {
        uiActions = new PlayerInputActions();
    }
    void Start()
    {
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.StopMusic();
        AudioManager.PlayMusic(MusicType.UIMenu);
    }

    void OnEnable()
    {
        uiActions.Enable();
        uiActions.UI.Cancel.performed += CancelClicked;
        uiActions.UI.Cancel.canceled += CancelClicked;
    }

    void OnDisable()
    {
        uiActions.UI.Cancel.performed -= CancelClicked;
        uiActions.UI.Cancel.canceled -= CancelClicked;
    }

    public void AccesClicked()
    {
        insideOptions = true;
        accessabilityOptions.SetActive(true);
        soundOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        eventSystem.SetSelectedGameObject(firstItems[0].gameObject);
        
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
        insideOptions = true;
        soundOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        languageOptions.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        eventSystem.SetSelectedGameObject(firstItems[1].gameObject);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void ControlClicked()
    {
        insideOptions = true;
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        languageOptions.SetActive(false);
        eventSystem.SetSelectedGameObject(firstItems[2].gameObject);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void LanguageClicked()
    {
        insideOptions = true;
        languageOptions.SetActive(true);
        accessabilityOptions.SetActive(false);
        soundOptions.SetActive(false);
        eventSystem.SetSelectedGameObject(firstItems[3].gameObject);
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

    public void CancelClicked(InputAction.CallbackContext ctx)
    {
        if (insideOptions)
        {
            AudioManager.PlaySFX(SoundType.UIConfirm);
            eventSystem.SetSelectedGameObject(firstItems[4].gameObject);
            insideOptions = false;
        }
    }
}
