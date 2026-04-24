using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelStartScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject levelText;
    private Vector2 playerVelocity = new(0, 2);
    private Vector2 enemyVelocity = new (0, -2);
    public static bool levelStarted;
    [SerializeField] private GameObject playerBoundries;
    [SerializeField] private GameObject enemyFactory;

    void Awake()
    {
        levelStarted = false;
        enemies = GameObject.FindGameObjectsWithTag("StartingEnemy");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemyTransition(1f));
        StartCoroutine(PlayerTransition(1f));
    }

    // Update is called once per frame
    void Update()
    {
        
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
                new Vector2(player.transform.position.x, player.transform.position.y + 2f), ref playerVelocity, 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        levelText.SetActive(false);
        levelStarted = true;
        playerBoundries.SetActive(true);
        enemyFactory.SetActive(true);

    }
}
