using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
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
        StartCoroutine(Level1());
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
            var enemyScript = enemies[i].GetComponent<EnemyScript>();
            if (enemyScript.following == false)
            {
                enemyScript.lerpPos = endPositions[endPositionIndex].position;
                enemyScript.FindPath(offsetStart, path);
                offsetStart += 0.03f;
                endPositionIndex++;
                enemyScript.following = true;
            }
        }
    }

    IEnumerator Level1()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(enemyList[3], new Vector2(0, -5), Quaternion.identity);
        }
        for (int i = 0; i < 6; i++)
        {
            Instantiate(enemyList[2], new Vector2(0, 5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemyList[1], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return null;
    }
}
