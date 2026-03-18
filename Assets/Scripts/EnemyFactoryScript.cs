using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactoryScript : MonoBehaviour
{
    public TimerManager timerScript;
    public List<GameObject> enemyGO;
    public List<SplineContainer> paths;
    public List<Transform> wave1Positions;
    public List<Transform> wave2Positions;
    public float offsetStart;
    [SerializeField] int currentPos;
    private int enemyCount;
    private float timerNO;
    public bool wave1Complete;
    private bool wave2Complete;

    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount = wave1Positions.Count;
        SpawnEnemies(0);
    }

    // Update is called once per frame
    void Update()
    {
        timerNO = timerScript.currentTime;

        if (timerNO < 54 && wave1Complete == false)
        {
            //currentPos = 0;
            enemyCount = wave2Positions.Count;
            SpawnEnemies(1);
            wave1Complete = true;
        }
    }

    void SpawnEnemies(int waveNO)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyGO[waveNO], new Vector3(0f, -5f, 0f), Quaternion.identity);
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
            switch (path)
            {
                case 0:
                    enemies[i].layer = 6;
                    break;
                case 1:
                    enemies[i].layer = 7;
                    break;
            }
            var enemyScript = enemies[i].GetComponent<EnemyScript>();
            enemyScript.FindPath(offsetStart, path);
            offsetStart += 0.05f;
        }
    }
}
