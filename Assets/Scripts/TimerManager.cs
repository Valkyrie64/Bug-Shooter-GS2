using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    //Video Reference: https://www.youtube.com/watch?v=u_n3NEi223E //

    [Header("Component")]
    public TMP_Text timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    // Start is called before the first frame update
    void Start()
    {
        //Creates a library reference point
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    // Update is called once per frame
    void Update()
    {
        Scene current = SceneManager.GetActiveScene();
        //? means if true
        //: means other
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        //Creates a stop limit for the timer
        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            //To change text color
            //timerText.color = Color.red;
            enabled = false;
        }

        //Ends level when time runs out
        if(currentTime <= 0)
        {
            currentTime = 0;
            if (current.name == "Level_1")
            {
                SceneManager.LoadScene(current.buildIndex - 1);
            }
            if (current.name == "Level_2")
            {
                SceneManager.LoadScene(current.buildIndex - 3);
            }
            if (current.name == "Boss_forReal")
            {
                SceneManager.LoadScene(current.buildIndex - 5);
            }
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}
//Setting the decimal point on Timer
public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal
}