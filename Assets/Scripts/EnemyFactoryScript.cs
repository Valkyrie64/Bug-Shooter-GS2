using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactoryScript : MonoBehaviour
{
    public TimerManager timerScript;
    public GameObject enemyGO;
    public List<SplineContainer> paths;
    public List<Transform> positions;
    public float offsetStart;
    private float timerNO;
    private bool wave2Complete;

    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies(0);
    }

    // Update is called once per frame
    void Update()
    {
        timerNO = timerScript.currentTime;

        if (timerNO < 54 && wave2Complete == false)
        {
            SpawnEnemies(1);
            wave2Complete = true;
        }
    }

    void SpawnEnemies(int waveNO)
    {
        for (int i = 0; i < 7; i++)
        {
            Instantiate(enemyGO, new Vector3(0f, -5f, 0f), Quaternion.identity);
        }
        LinkToPath(waveNO);
    }

    //Tells enemies where to be on the path
    void LinkToPath(int path)
    {
        offsetStart = 0;
        var enemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemyScript = enemies[i].GetComponent<EnemyScript>();
            enemyScript.FindPath(offsetStart, path);
            offsetStart += 0.05f;
        }
    }

    public void SetEnemyPositions()
    {
        var enemies = GameObject.FindGameObjectsWithTag("StoppedEnemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemyScript = enemies[i].GetComponent<EnemyScript>();
            enemyScript.FindPosition(positions[i]);
        }
    }
}
