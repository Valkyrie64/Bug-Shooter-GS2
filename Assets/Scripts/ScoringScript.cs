using UnityEngine;
using TMPro;
using System.Collections;

public class ScoringScript : MonoBehaviour
{
    public float scoreNumber;
    public TMP_Text scoreText;
    public float timer;
    public TMP_Text rankText;
    public float mult;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {scoreNumber.ToString()}";
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0f)
        {
            timer = 0f;
        }
        if (timer > 70f)
        {
            timer = 70f;
        }
        //Rank Checker
        switch (timer)
        {
            case <= 0f:
                rankText.text = $"Rank: D";
                mult = 1f;
                break;
            case <= 20f:
                rankText.text = $"Rank: C";
                mult = 1.5f;
                break;
            case <= 40f:
                rankText.text = $"Rank: B";
                mult = 2f;
                break;
            case <= 60f:
                rankText.text = $"Rank: A";
                mult = 2.5f;
                break;
            case > 60f:
                rankText.text = $"Rank: S";
                mult = 3f;
                break;

        }
    }

    public void ScoreUpdate(float score)
    {
        scoreNumber += score * mult;
        timer += 10;
    }
}
