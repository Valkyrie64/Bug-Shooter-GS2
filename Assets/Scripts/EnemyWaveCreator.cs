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
        StartCoroutine(Stage6());
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

    IEnumerator Stage1()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyList[3], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(8f);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyList[3], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 1; i++)
        {
            Instantiate(enemyList[4], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(2);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[3], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(3);
        yield return new WaitForSeconds(8f);
        for (int i = 0; i < 6; i++)
        {
            Instantiate(enemyList[2], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(4);
        yield return null;
    }

        IEnumerator Stage2()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyList[1], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(8f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[2], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return new WaitForSeconds(8f);
        for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyList[5], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(2);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[1], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(3);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyList[5], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(4);
        yield return null;
    }

     IEnumerator Stage4()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[2], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyList[9], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(2);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[9], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(3);
        yield return null;
    }

        IEnumerator Stage5()
    {
        for (int i = 0; i < 1; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 1; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 1; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(2);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(3);
        yield return null;
    }

     IEnumerator Stage6()
    {
       for (int i = 0; i < 2; i++)
        {
            Instantiate(enemyList[7], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(0);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyList[4], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(1);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 1; i++)
        {
            Instantiate(enemyList[5], new Vector2(0, -5), Quaternion.identity);
        }
        LinkToPath(2);
        yield return new WaitForSeconds(3f);
        yield return null;
    }
}
