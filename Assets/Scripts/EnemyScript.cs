using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyScript : MonoBehaviour
{
    public ScoringScript scoreScript;
    public EnemyFactoryScript factoryScript;
    public SplineAnimate splineScript;
    public float scoreValue;
    private bool following;
    private Vector3 lerpPos;
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
        //Moves enemies smoothly to set position
        if (following == false)
        {
            transform.position = Vector3.Lerp(transform.position, lerpPos, (Time.time * 2f) * Time.deltaTime);
        }
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
            splineScript.Container = null;
            factoryScript.SetEnemyPositions();
        }
    }

    
}
