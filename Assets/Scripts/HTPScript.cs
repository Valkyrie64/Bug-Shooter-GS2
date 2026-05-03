using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPScript : MonoBehaviour
{
    [SerializeField] private GameObject controlsOverlay;
    [SerializeField] private GameObject levelsOverlay;
    [SerializeField] private GameObject scoringOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlsOverlay.SetActive(false);
        levelsOverlay.SetActive(false);
        scoringOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlsClicked()
    {
        controlsOverlay.SetActive(true);
        levelsOverlay.SetActive(false);
        scoringOverlay.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void LevelsClicked()
    {
        levelsOverlay.SetActive(true);
        controlsOverlay.SetActive(false);
        scoringOverlay.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void ScoringClicked()
    {
        scoringOverlay.SetActive(true);
        controlsOverlay.SetActive(false);
        levelsOverlay.SetActive(false);
        AudioManager.PlaySFX(SoundType.UIConfirm);
    }

    public void BackClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:0);
    }
}
