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
        following = true;
        var scoreGO = GameObject.Find("ScoringGO");
        scoreScript = scoreGO.GetComponent<ScoringScript>();
        var factoryGO = GameObject.Find("EnemyFactory");
        factoryScript = factoryGO.GetComponent<EnemyFactoryScript>();
        splineScript = this.GetComponent<SplineAnimate>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (following == false)
        {
            transform.position = Vector3.Lerp(transform.position, lerpPos, 1f * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        scoreScript.ScoreUpdate(scoreValue);
        Destroy(gameObject);
    }

    public void FindPath(float offset)
    {
        splineScript.Container = factoryScript.paths[0];
        splineScript.StartOffset = offset;
        splineScript.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemySpace"))
        {
            splineScript.Container = null;
            SetEnemyPositions();
        }
    }

    public void SetEnemyPositions()
    {
        float spaceOffset = 0;
        float lerpSpeed = 2f;
        float t = Time.time * lerpSpeed;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            var enemyScript = enemy.GetComponent<EnemyScript>();
            lerpPos = new Vector3(-1.16f + spaceOffset, 1.78f, 0);
            spaceOffset += 1f;
            enemyScript.following = false;
        }
    }
}
