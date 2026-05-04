using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject technicianRobot;
    private bool created;
    
    public Animator animator;
    private float animTimer = 0;
    [SerializeField] private AnimatorOverrideController[] arachnophobiaControllers;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] arachnophobiaSprites;

    private TimerManager timerScript;
    
    

    public enum AttackType
    {
        Straight_Shot,
        Tracking_Shot,
        Barrage_Shot,
        Wave_Shot,
        Kamikaze,
        Round_Shot,
        Quad_Shot,
        Wall_Shot,
        Create_Robot
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        kamikaze = false;
        var scoreGO = GameObject.Find("ScoringGO");
        scoreScript = scoreGO.GetComponent<ScoringScript>();
        timerScript = GameObject.Find("Timer").GetComponent<TimerManager>();
        if (gameObject.tag != "StartingEnemy")
        {
            var factoryGO = GameObject.Find("EnemyFactory");
            waveScript = factoryGO.GetComponent<EnemyWaveCreator>();
            splineScript = this.GetComponent<SplineAnimate>();
        }
        var posGO = GameObject.Find("EnemyEndPositions");
        
        animTimer = Random.Range(2, 8);
        rand = Random.Range(2, 5);
    }

    private void Start()
    {
        if (gameObject.tag == "StartingEnemy")
        {
            following = true;
        }
        if (gameObject.layer == LayerMask.NameToLayer("SpiderLayer") && PlayerPrefs.GetInt("Arachnophobia") == 1)
        {
            Debug.Log("Spiders Off");
            if (gameObject.name.Contains("Black"))
            {
                animator.runtimeAnimatorController = arachnophobiaControllers[0];
                animator.SetBool("Arachnophobia", true);
            }
            if (gameObject.name.Contains("Flag"))
            {
                animator.runtimeAnimatorController = arachnophobiaControllers[1];
                animator.SetBool("Arachnophobia", true);
            }
            if (gameObject.name.Contains("Red"))
            {
                animator.runtimeAnimatorController = arachnophobiaControllers[2];
                animator.SetBool("Arachnophobia", true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerScript.currentTime > 0)
        {
            attackTimer += Time.deltaTime;
            animTimer -= Time.deltaTime;
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
                transform.Translate(Vector2.down * (4f * Time.deltaTime));
            }
            //Animation
            if (animTimer <= 0)
            {
                BlinkAnimation();
            }
        }
        
    }

    void BlinkAnimation()
    {
        animator.Play("Blink");
        animTimer = Random.Range(2, 8);
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
                    rand = Random.Range(4f, 5f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                    AudioManager.PlaySFX(SoundType.StraightShot);
                }
                break;
            case AttackType.Tracking_Shot: //Shot Follows Player
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(6.5f, 7f);
                    attackTimer = 0;
                    bullet.tag = "TrackingBullet";
                    Instantiate(bullet, transform.position, transform.rotation);
                    AudioManager.PlaySFX(SoundType.TrackingShot);
                }
                break;
            case AttackType.Barrage_Shot: //Shoots 5 bullets
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(4f, 6f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    AudioManager.PlaySFX(SoundType.BarrageShot);
                    StartCoroutine(BarrageAttack());
                }
                break;
            case AttackType.Wave_Shot: //Shoots 3 bullets outward
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(4f, 5f);
                    attackTimer = 0;
                    bullet.tag = "EnemyBullet";
                    AudioManager.PlaySFX(SoundType.WaveShot);
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -10f));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 10f));
                }
                break;
            case AttackType.Kamikaze: //Moves downward then explodes
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    kamikaze = true;
                    AudioManager.PlaySFX(SoundType.KamikazeShot);
                }
                break;
            case AttackType.Round_Shot: //Shoots bullets around itself
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    AudioManager.PlaySFX(SoundType.RoundShot);
                    StartCoroutine(RoundShot());
                }
                break;
            case AttackType.Quad_Shot: //Shoots 2 rows of bullets
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    AudioManager.PlaySFX(SoundType.WallShot);
                    StartCoroutine(QuadShot());
                }
                break;
            case AttackType.Wall_Shot: //Shoots 4 rows of bullets
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand)
                {
                    rand = Random.Range(2.5f, 3f);
                    attackTimer = 0;
                    AudioManager.PlaySFX(SoundType.WallShot);
                    StartCoroutine(WallShot());
                }
                break;
            case AttackType.Create_Robot:
                if ((gameObject.tag == "StoppedEnemy" || gameObject.tag == "StartingEnemy") && attackTimer >= rand && created == false)
                {
                    rand = Random.Range(2.5f, 3f);
                    Instantiate(technicianRobot, new Vector2(transform.position.x, transform.position.y - 2f), transform.rotation);
                    AudioManager.PlaySFX(SoundType.CreateShot);
                    created = true;
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
        AudioManager.PlaySFX(SoundType.Explosion);
        scoreValue = 0f;
        Destroy(gameObject);
    }

    public IEnumerator RoundShot()
    {
        for (int i = 0; i < 360; i += 45)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
            yield return null;
        }
    }
    
    public IEnumerator QuadShot()
    {
        for (int i = 0; i < 2; i ++)
        {
            Instantiate(bullet, new Vector2(transform.position.x - 0.2f, transform.position.y), transform.rotation);
            Instantiate(bullet, new Vector2(transform.position.x + 0.2f, transform.position.y), transform.rotation);
            yield return new WaitForSeconds(0.08f);
        }
    }

    public IEnumerator WallShot()
    {
        for (int i = 0; i < 4; i ++)
        {
            Instantiate(bullet, new Vector2(transform.position.x - 0.6f, transform.position.y), transform.rotation);
            Instantiate(bullet, new Vector2(transform.position.x - 0.2f, transform.position.y), transform.rotation);
            Instantiate(bullet, new Vector2(transform.position.x + 0.2f, transform.position.y), transform.rotation);
            Instantiate(bullet, new Vector2(transform.position.x + 0.6f, transform.position.y), transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

