using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public ScoringScript scoreScript;
    public EnemyFactoryScript factoryScript;
    public SplineAnimate splineScript;
    public float scoreValue;
    public GameObject bullet;
    public EnemyManagerScriptableObject enemyManager;
    private bool following;
    private Vector3 lerpPos;
    private float attackTimer;
    private float rand;
    private int attackPattern;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameObject.tag = "FlyingEnemy";
        following = true;
        var scoreGO = GameObject.Find("ScoringGO");
        scoreScript = scoreGO.GetComponent<ScoringScript>();
        var factoryGO = GameObject.Find("EnemyFactory");
        factoryScript = factoryGO.GetComponent<EnemyFactoryScript>();
        splineScript = this.GetComponent<SplineAnimate>();
        var posGO = GameObject.Find("EnemyEndPositions");

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        //Moves enemies smoothly to set position
        if (following == false)
        {
            transform.position = Vector3.Lerp(transform.position, lerpPos, (Time.time * 2f) * Time.deltaTime);
            EnemyAttack(attackPattern);
        }
        //Shooting Timer
        /*timer += Time.deltaTime;
        
        }*/
    }

    private void OnDisable()
    {
        scoreScript.ScoreUpdate(scoreValue);
        Destroy(gameObject);
    }

    public void FindPath(float offset, int path)
    {
        //Tells the enemy what path to follow
        splineScript.Container = factoryScript.paths[path];
        splineScript.StartOffset = offset;
        splineScript.Play();
        enemyManager.SetEnemyType(path);
        EnemySetup(path);
    }

    public void EnemySetup(int setupVal)
    {
        scoreValue = enemyManager.pointsValue;
        var spriteRend = gameObject.GetComponent<SpriteRenderer>();
        spriteRend.sprite = enemyManager.enemySprites[setupVal];
        attackPattern = enemyManager.attackPattern;
    }

    public void FindPosition(Transform posToGo)
    {
        //Tells enemy where to go after following path
        lerpPos = posToGo.position;
        following = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemySpace"))
        {
            this.gameObject.tag = "StoppedEnemy";
            rand = Random.Range(1.5f, 2.5f);
            attackTimer = 0;
            splineScript.Container = null;
            factoryScript.SetEnemyPositions();
        }
    }

    void EnemyAttack(int attackType)
    {
        switch (attackType)
        {
            case 1: //Shoot Straight Ahead
                if (gameObject.tag == "StoppedEnemy" && attackTimer >= rand)
                {
                    rand = Random.Range(1.8f, 2.2f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                }
                break;
            case 2: //Shot Follows Player
                if (gameObject.tag == "StoppedEnemy" && attackTimer >= rand)
                {
                    rand = Random.Range(0.8f, 1.2f);
                    attackTimer = 0;
                    bullet.tag = "TrackingBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                }
                break;
        }
    }
}
