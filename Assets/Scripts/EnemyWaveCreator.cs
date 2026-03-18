using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class EnemyWaveCreator : MonoBehaviour
{
    public TimerManager timerScript;
    private float timerNO;
    
    public List<GameObject> enemyList;
    public List<SplineContainer> paths;
    public List<Transform> endPositions;
    private int endPositionIndex = 0;
    private float offsetStart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Stage1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
            enemyScript.lerpPos = endPositions[endPositionIndex].position;
            enemyScript.FindPath(offsetStart, path);
            offsetStart += 0.05f;
            endPositionIndex++;
        }
    }

    IEnumerator Stage1()
    {
        for (int i = 0; i < 12; i++)
        {
            Instantiate(enemyList[3], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(6f);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemyList[1], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return null;
    }
}
