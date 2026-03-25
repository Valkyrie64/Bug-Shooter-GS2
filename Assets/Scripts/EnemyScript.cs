using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Info")] public float health;
    public float scoreValue;
    public AttackType attackPattern;

    public ScoringScript scoreScript;
    public EnemyWaveCreator waveScript;
    public SplineAnimate splineScript;
    public GameObject bullet;

    public bool following;
    private bool kamikaze;
    public Vector3 lerpPos;
    [SerializeField] float attackTimer;
    private float rand;

    public enum AttackType
    {
        Straight_Shot,
        Tracking_Shot,
        Barrage_Shot,
        Wave_Shot,
        Kamikaze
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        kamikaze = false;
        var scoreGO = GameObject.Find("ScoringGO");
        scoreScript = scoreGO.GetComponent<ScoringScript>();
        var factoryGO = GameObject.Find("EnemyFactory");
        waveScript = factoryGO.GetComponent<EnemyWaveCreator>();
        splineScript = this.GetComponent<SplineAnimate>();
        var posGO = GameObject.Find("EnemyEndPositions");

    }

    private void Start()
    {
        if (gameObject.tag == "StartingEnemy")
        {
            following = true;
        }

        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        //Moves enemies smoothly to set position
        if (following == false && kamikaze == false)
        {
            transform.position = Vector3.Lerp(transform.position, lerpPos, (Time.time * 2f) * Time.deltaTime);
            EnemyAttack(attackPattern);
        }

        if (gameObject.tag == "StartingEnemy")
        {
            EnemyAttack(attackPattern);
        }

        if (kamikaze)
        {
            transform.Translate(Vector2.down * (4 * Time.deltaTime));
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
        splineScript.Container = waveScript.paths[path];
        splineScript.StartOffset = offset;
        splineScript.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemySpace"))
        {
            gameObject.tag = "StoppedEnemy";
            rand = Random.Range(1.5f, 2.5f);
            attackTimer = 0;
            splineScript.Container = null;
            following = false;
        }

        if (attackPattern == AttackType.Kamikaze && collision.CompareTag("ExplosionSpace"))
        {
            bullet.tag = "EnemyBullet";
            StartCoroutine(KamikazeAttack());
        }
    }

    void EnemyAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Straight_Shot: //Shoot Straight Ahead
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                }

                break;
            case AttackType.Tracking_Shot: //Shot Follows Player
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    bullet.tag = "TrackingBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                }

                break;
            case AttackType.Barrage_Shot:
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    StartCoroutine(BarrageAttack());
                }

                break;
            case AttackType.Wave_Shot:
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -10f));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 10f));
                }

                break;
            case AttackType.Kamikaze:
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    kamikaze = true;
                }
                break;
        }
    }

    public IEnumerator BarrageAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.04f);
        }
    }

    public IEnumerator KamikazeAttack()
    {
        for (int i = 0; i < 360; i += 45)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
            yield return null;
        }
        scoreValue = 0f;
        Destroy(gameObject);
    }
}

