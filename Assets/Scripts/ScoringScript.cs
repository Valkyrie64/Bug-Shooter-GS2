using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour
{
    public float scoreNumber;
    public TMP_Text scoreText;
    public float timer;
    public float mult;
    public Image rankImage;
    public List<Sprite> rankSprites;
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
        //Rank Timer Bounds
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
                rankImage.sprite = rankSprites[0];
                mult = 1f;
                break;
            case <= 20f:
                rankImage.sprite = rankSprites[1];
                mult = 1.5f;
                break;
            case <= 40f:
                rankImage.sprite = rankSprites[2];
                mult = 2f;
                break;
            case <= 60f:
                rankImage.sprite = rankSprites[3];
                mult = 2.5f;
                break;
            case > 60f:
                rankImage.sprite = rankSprites[4];
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
