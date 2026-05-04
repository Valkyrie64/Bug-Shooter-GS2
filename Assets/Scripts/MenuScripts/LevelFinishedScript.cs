using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedScript : MonoBehaviour
{
    private int lastSceneIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSceneIndex = PlayerPrefs.GetInt("LastLevelScene");
    }

    public void RetryClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:lastSceneIndex);
    }

    public void TitleClicked()
    {
        AudioManager.PlaySFX(SoundType.UIConfirm);
        SceneManager.LoadScene(sceneBuildIndex:0);
    }
}
