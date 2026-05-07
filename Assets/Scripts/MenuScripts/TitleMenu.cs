using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private GameObject titleAnt;
    [SerializeField] private SpriteRenderer bugImage;
    [SerializeField] private Sprite[] sprites;
    [Header("Title Collections")]
    [SerializeField] private GameObject startingOptions;
    [SerializeField] private GameObject levelSelection;
    
    [Header("World Collections")]
    [SerializeField] private GameObject worldSelect;
    [SerializeField] private GameObject world1;
    [SerializeField]private Button world1Button;
    [SerializeField] private Navigation world1Navigation;
    [SerializeField] private GameObject world2;
    [SerializeField]private Button world2Button;
    [SerializeField] private Navigation world2Navigation;
    [SerializeField] private GameObject world3;
    [SerializeField]private Button world3Button;
    [SerializeField] private Navigation world3Navigation;
    [SerializeField] private GameObject backGO;
    [SerializeField] private Button backButton;
    [SerializeField] private Navigation backNavigation;

    private Button level2;
    private Button level3;
    
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable[] firstItems;
    private Selectable nextSelection;
    private Selectable backSelection;
    
    [SerializeField] private SpriteRenderer backgroundImage;
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private GameObject titleImage;
    private void Awake()
    {
        startingOptions.SetActive(true);
        levelSelection.SetActive(false);
        titleAnt.SetActive(true);
        bugImage.gameObject.SetActive(false);
        backgroundImage.sprite = backgroundSprites[0];
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
        //level2.interactable = false;
        //level3.interactable = false;
    }

    public void PlayClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        startingOptions.SetActive(false);
        levelSelection.SetActive(true);
        world1.SetActive(true);
        world2.SetActive(false);
        world3.SetActive(false);
        level2 = GameObject.Find("W1Level-2").GetComponent<Button>();
        level3 = GameObject.Find("W1Level-3").GetComponent<Button>();
        titleAnt.SetActive(false);
        bugImage.gameObject.SetActive(true);
        bugImage.sprite = sprites[0];
        backgroundImage.sprite = backgroundSprites[1];
        titleImage.SetActive(false);
        eventSystem.SetSelectedGameObject(firstItems[1].gameObject);
        nextSelection = GameObject.Find("W1Level-1").GetComponent<Selectable>();
        world1Navigation.selectOnDown = nextSelection;
        world1Button.navigation = world1Navigation;
        world2Navigation.selectOnDown = nextSelection;
        world2Button.navigation = world2Navigation;
        world3Navigation.selectOnDown = nextSelection;
        world3Button.navigation = world3Navigation;
        var selectableButtons = GameObject.FindGameObjectsWithTag("Level-Buttons");
        var level3Button = selectableButtons[2].gameObject.GetComponent<Button>();
        var level2Button = selectableButtons[1].gameObject.GetComponent<Button>();
        var level1Button = selectableButtons[0].gameObject.GetComponent<Button>();

        var level3Lock = level3Button.GetComponentInChildren<Image>();
        var level2Lock = level2Button.GetComponentInChildren<Image>();
        if (PlayerPrefs.GetInt("W1L2") == 1 && PlayerPrefs.GetInt("W1L3") == 1)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = true;
            backSelection = GameObject.Find("W1Level-3").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level2Lock.gameObject.SetActive(false);
            level3Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W1L2") == 1 && PlayerPrefs.GetInt("W1L3") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W1Level-2").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level2Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W1L2") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = false;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W1Level-1").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
        }
    }

    public void VersusClicked()
    {
        
    }

    public void BackClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        startingOptions.SetActive(true);
        levelSelection.SetActive(false);
        titleAnt.SetActive(true);
        bugImage.gameObject.SetActive(false);
        backgroundImage.sprite = backgroundSprites[0];
        titleImage.SetActive(true);
        eventSystem.SetSelectedGameObject(firstItems[0].gameObject);
    }

    public void World1Clicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        world1.SetActive(true);
        world2.SetActive(false);
        world3.SetActive(false);
        level2 = GameObject.Find("W1Level-2").GetComponent<Button>();
        level3 = GameObject.Find("W1Level-3").GetComponent<Button>();
        bugImage.sprite = sprites[0];
        nextSelection = GameObject.Find("W1Level-1").GetComponent<Selectable>();
        world1Navigation.selectOnDown = nextSelection;
        world1Button.navigation = world1Navigation;
        world2Navigation.selectOnDown = nextSelection;
        world2Button.navigation = world2Navigation;
        world3Navigation.selectOnDown = nextSelection;
        world3Button.navigation = world3Navigation;
        var selectableButtons = GameObject.FindGameObjectsWithTag("Level-Buttons");
        var level3Button = selectableButtons[2].gameObject.GetComponent<Button>();
        var level2Button = selectableButtons[1].gameObject.GetComponent<Button>();
        var level1Button = selectableButtons[0].gameObject.GetComponent<Button>();
        
        var level3Lock = level3Button.GetComponentInChildren<Image>();
        var level2Lock = level2Button.GetComponentInChildren<Image>();
        if (PlayerPrefs.GetInt("W1L2") == 1 && PlayerPrefs.GetInt("W1L3") == 1)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = true;
            backSelection = GameObject.Find("W1Level-3").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level3Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W1L2") == 1 && PlayerPrefs.GetInt("W1L3") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W1Level-2").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level2Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W1L2") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = false;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W1Level-1").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
        }
    }

    public void World2Clicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        world2.SetActive(true);
        world3.SetActive(false);
        world1.SetActive(false);
        //level2 = GameObject.Find("W2Level-2").GetComponent<Button>();
        //level3 = GameObject.Find("W2Level-3").GetComponent<Button>();
        bugImage.sprite = sprites[1];
        nextSelection = GameObject.Find("W2Level-1").GetComponent<Selectable>();
        world1Navigation.selectOnDown = nextSelection;
        world1Button.navigation = world1Navigation;
        world2Navigation.selectOnDown = nextSelection;
        world2Button.navigation = world2Navigation;
        world3Navigation.selectOnDown = nextSelection;
        world3Button.navigation = world3Navigation;
        var selectableButtons = GameObject.FindGameObjectsWithTag("Level-Buttons");
        var level3Button = selectableButtons[2].gameObject.GetComponent<Button>();
        var level2Button = selectableButtons[1].gameObject.GetComponent<Button>();
        var level1Button = selectableButtons[0].gameObject.GetComponent<Button>();
        
        
        
        if (PlayerPrefs.GetInt("W2L3") == 1)
        { 
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = true;
            backSelection = GameObject.Find("W2Level-3").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            var level3Lock = level3Button.GetComponentInChildren<Image>();
            level3Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W2L2") == 1)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W2Level-2").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            var level2Lock = level2Button.GetComponentInChildren<Image>();
            level2Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W2L2") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = false;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W2Level-1").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
        }
    }

    public void World3Clicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        world3.SetActive(true);
        world1.SetActive(false);
        world2.SetActive(false);
        level2 = GameObject.Find("W3Level-2").GetComponent<Button>();
        level3 = GameObject.Find("W3Level-3").GetComponent<Button>();
        var arachno = PlayerPrefs.GetInt("Arachnophobia");
        if (arachno == 0)
        {
            bugImage.sprite = sprites[2];
        }
        if (arachno == 1)
        {
            bugImage.sprite = sprites[3];
        }
        nextSelection = GameObject.Find("W3Level-1").GetComponent<Selectable>();
        world1Navigation.selectOnDown = nextSelection;
        world1Button.navigation = world1Navigation;
        world2Navigation.selectOnDown = nextSelection;
        world2Button.navigation = world2Navigation;
        world3Navigation.selectOnDown = nextSelection;
        world3Button.navigation = world3Navigation;
        var selectableButtons = GameObject.FindGameObjectsWithTag("Level-Buttons");
        var level3Button = selectableButtons[2].gameObject.GetComponent<Button>();
        var level2Button = selectableButtons[1].gameObject.GetComponent<Button>();
        var level1Button = selectableButtons[0].gameObject.GetComponent<Button>();
        
        var level3Lock = level3Button.GetComponentInChildren<Image>();
        var level2Lock = level2Button.GetComponentInChildren<Image>();
        if (PlayerPrefs.GetInt("W3L3") == 1)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = true;
            backSelection = GameObject.Find("W3Level-3").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level3Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W3L2") == 1)
        {
            level1Button.interactable = true;
            level2Button.interactable = true;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W3Level-2").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
            level2Lock.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("W3L2") == 0)
        {
            level1Button.interactable = true;
            level2Button.interactable = false;
            level3Button.interactable = false;
            backSelection = GameObject.Find("W3Level-1").GetComponent<Selectable>();
            backNavigation.selectOnUp = backSelection;
            backButton.navigation = backNavigation;
        }
    }

    public void World1Level1Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 1);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void World1Level2Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 2);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:2);
    }
    
    public void World1Level3Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 3);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:3);
    }
    
    public void World2Level1Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 4);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:4);
    }
    
    public void World2Level2Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 5);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:5);
    }
    
    /*public void World2Level3Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 6);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:6);
    }*/
    
    public void World3Level1Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 6);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:6);
    }
    
    /*public void World3Level2Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 8);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:8);
    }*/
    
    /*public void World3Level3Clicked()
    {
        PlayerPrefs.SetInt("LastLevelScene", 9);
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:9);
    }*/
    
    public void HTPClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:8);
    }

    public void OptionsClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:9);
    }

    public void CreditsClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:10);
    }

    public void ExitClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        Application.Quit();
    }
}
