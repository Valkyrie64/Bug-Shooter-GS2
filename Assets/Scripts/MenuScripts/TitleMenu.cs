using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }

    public void OptionsClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex:2);
    }

    public void CreditsClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex:3);
    }

    public void ExitClicked()
    {
        Application.Quit();
    }
}
