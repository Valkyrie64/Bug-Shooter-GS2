using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactoryScript : MonoBehaviour
{
    public GameObject enemyGO;
    public List<SplineContainer> paths;
    public float offsetStart;
    private Vector3 spawnPos = new Vector3(0, -5, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(enemyGO, new Vector3(0f, -5f, 0f), Quaternion.identity);
        }
        LinkToPath();
    }

    void LinkToPath()
    {
        offsetStart = 0;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemyScript = enemies[i].GetComponent<EnemyScript>();
            enemyScript.FindPath(offsetStart);
            offsetStart += 0.05f;
        }
    }
}
