using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D playerRb;
    [SerializeField] GameObject[] enemies;
    [SerializeField] private GameObject boss;
    [SerializeField] GameObject levelText;
    [SerializeField] GameObject clearText;
    private Vector2 playerVelocity = new(0, 2);
    private Vector2 enemyVelocity = new (0, -2);
    public static bool levelStarted;
    [SerializeField] private GameObject playerBoundries;
    [SerializeField] private GameObject enemyFactory;
    private string currentScene;
    private TimerManager timerScript;
    private ScoringScript scoringScript;
    public List<GameObject> savedEnemiesList;

    void Awake()
    {
        levelStarted = false;
        enemies = GameObject.FindGameObjectsWithTag("StartingEnemy");
        currentScene = SceneManager.GetActiveScene().name;
        GameObject timerGO = GameObject.Find("Timer");
        timerScript = timerGO.GetComponent<TimerManager>();
        scoringScript = GameObject.Find("ScoringGO").GetComponent<ScoringScript>();
        playerRb = player.GetComponent<Rigidbody2D>();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currentScene == "W1Level-1" || currentScene == "W2Level-1" || currentScene == "W3Level-1")
        {
            AudioManager.PlayMusic(MusicType.Level1Music);
            StartCoroutine(EnemyTransition(1f));
            //StartCoroutine(JesterTransition());
            StartCoroutine(PlayerTransition(1f));
        }

        if (currentScene == "W1Level-2" || currentScene == "W2Level-2" || currentScene == "W3Level-2")
        {
            AudioManager.PlayMusic(MusicType.Level2Music);
            StartCoroutine(EnemyTransition(1f));
            StartCoroutine(PlayerTransition(1f));
        }

        if (currentScene == "W1Level-3" || currentScene == "W2Level-3" || currentScene == "W3Level-3")
        {
            AudioManager.PlayMusic(MusicType.BossMusic);
            StartCoroutine(JesterTransition());
            //StartCoroutine(PlayerTransition(1f));
            
        }
        
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (timerScript.currentTime <= 0f && savedEnemiesList.Count <= 0)
        {
            GameWon();
        }

        if (timerScript.currentTime <= 0f && savedEnemiesList.Count > 0)
        {
            GameLost();
        }
    }

    public void GameLost()
    {
        StartCoroutine(LoseTransition());
    }

    public void GameWon()
    {
        SaveScoreData();
        StartCoroutine(WinTransition());
    }

    IEnumerator EnemyTransition(float animTime)
    {
        float timer = 0f;
        while (timer < animTime)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.position = Vector2.SmoothDamp(enemies[i].transform.position,
                    new Vector2(enemies[i].transform.position.x, enemies[i].transform.position.y - 2f), ref enemyVelocity, 1f);
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator PlayerTransition(float animTime)
    {
        float timer = 0f;
        while (timer < animTime)
        {
            player.transform.position = Vector2.SmoothDamp(player.transform.position,
                new Vector2(player.transform.position.x, player.transform.position.y + 3f), ref playerVelocity, 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        levelText.SetActive(false);
        levelStarted = true;
        playerBoundries.SetActive(true);
        enemyFactory.SetActive(true);
    }

    IEnumerator JesterTransition()
    {
        var antQueen = GameObject.FindGameObjectWithTag("QueenAnt");
        var queenAnimator = antQueen.GetComponent<Animator>();
        var jesterAnt = GameObject.FindGameObjectWithTag("BossEnemy");
        float timer = 0f;
        while (timer < 1f)
        {
            antQueen.transform.position = Vector2.SmoothDamp(antQueen.transform.position,
                new Vector2(antQueen.transform.position.x, antQueen.transform.position.y - 3f), ref enemyVelocity, 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        queenAnimator.SetBool("Summon", true);
        yield return new WaitForSeconds(0.5f);
        while (timer < 3.5f)
        {
            antQueen.transform.position = Vector2.SmoothDamp(antQueen.transform.position,
                new Vector2(antQueen.transform.position.x, antQueen.transform.position.y + 2f), ref enemyVelocity, 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        while (timer < 5.1f)
        {
            jesterAnt.transform.position = Vector2.SmoothDamp(jesterAnt.transform.position,
                new Vector2(jesterAnt.transform.position.x, jesterAnt.transform.position.y - 2f), ref enemyVelocity, 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        levelText.SetActive(false);
        levelStarted = true;
        playerBoundries.SetActive(true);
        enemyFactory.SetActive(true);
    }

    void SaveScoreData()
    {
        float currentScore = scoringScript.scoreNumber;
        switch (currentScene)
        {
            case "W1Level-1":
                if (PlayerPrefs.GetFloat("W1L1-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W1L1-Score", currentScore);
                }
                break;
            case "W1Level-2":
                if (PlayerPrefs.GetFloat("W1L2-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W1L2-Score", currentScore);
                }
                break;
            case "W1Level-3":
                if (PlayerPrefs.GetFloat("W1L3-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W1L3-Score", currentScore);
                }
                break;
            case "W2Level-1":
                if (PlayerPrefs.GetFloat("W2L1-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W2L1-Score", currentScore);
                }
                break;
            case "W2Level-2":
                if (PlayerPrefs.GetFloat("W2L2-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W2L2-Score", currentScore);
                }
                break;
            case "W2Level-3":
                if (PlayerPrefs.GetFloat("W2L3-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W2L3-Score", currentScore);
                }
                break;
            case "W3Level-1":
                if (PlayerPrefs.GetFloat("W3L1-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W3L1-Score", currentScore);
                }
                break;
            case "W3Level-2":
                if (PlayerPrefs.GetFloat("W3L2-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W3L2-Score", currentScore);
                }
                break;
            case "W3Level-3":
                if (PlayerPrefs.GetFloat("W3L3-Score") < currentScore)
                {
                    PlayerPrefs.SetFloat("W3L3-Score", currentScore);
                }
                break;
        }
    }

    IEnumerator WinTransition()
    {
        playerRb.linearVelocity = Vector2.zero;
        levelStarted = false;
        clearText.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(sceneBuildIndex:14);
    }

    IEnumerator LoseTransition()
    {
        playerRb.linearVelocity = Vector2.zero;
        levelStarted = false;
        clearText.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(sceneBuildIndex:13);
    }
}
