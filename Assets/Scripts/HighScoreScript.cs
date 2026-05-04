using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighScoreScript : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private EventSystem eventSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (eventSystem.currentSelectedGameObject.name)
        {
            case "W1Level-1":
                highScoreText.text = PlayerPrefs.GetFloat("W1L1-Score").ToString();
                break;
            case "W1Level-2":
                highScoreText.text = PlayerPrefs.GetFloat("W1L2-Score").ToString();
                break;
            case "W1Level-3":
                highScoreText.text = PlayerPrefs.GetFloat("W1L3-Score").ToString();
                break;
            case "W2Level-1":
                highScoreText.text = PlayerPrefs.GetFloat("W2L1-Score").ToString();
                break;
            case "W2Level-2":
                highScoreText.text = PlayerPrefs.GetFloat("W2L2-Score").ToString();
                break;
            case "W2Level-3":
                highScoreText.text = PlayerPrefs.GetFloat("W2L3-Score").ToString();
                break;
            case "W3Level-1":
                highScoreText.text = PlayerPrefs.GetFloat("W3L1-Score").ToString();
                break;
            case "W3Level-2":
                highScoreText.text = PlayerPrefs.GetFloat("W3L2-Score").ToString();
                break;
            case "W3Level-3":
                highScoreText.text = PlayerPrefs.GetFloat("W3L3-Score").ToString();
                break;
        }
    }
}
