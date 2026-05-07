using UnityEngine;
using System.Collections;

public class JesterAntScript : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;
    private int dirSpeed = 5;
    private int bossHealth = 15;
    private LevelStartScript levelStartScript;
    public ScoringScript scoreScript;
    [SerializeField] private float scoreValue;
    [SerializeField] private Animator animator;
    [SerializeField]private float animateTimer;
    [SerializeField]private float attackTimer;
    [SerializeField]private float randTime;
    private int randBullet;
    private int randAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelStartScript = GameObject.Find("ManagerGO").GetComponent<LevelStartScript>();
        levelStartScript.savedEnemiesList.Add(gameObject);
        randTime = Random.Range(1.2f, 1.5f);
        randAttack = Random.Range(1, 6);
        Debug.Log(randAttack);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(randAttack);
        if (LevelStartScript.levelStarted)
        {
            animateTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            transform.Translate(Vector2.left * (Time.deltaTime * dirSpeed));
            if (bossHealth <= 0)
            {
                StartCoroutine(DeathSequence());
            }
            
            if (attackTimer >= randTime && randAttack <= 3)
            {
                randTime = Random.Range(1.2f, 1.5f);
                randBullet = Random.Range(0, 5);
                randAttack = Random.Range(1, 6);
                attackTimer = 0;
                Instantiate(bullet[randBullet], transform.position, transform.rotation);
                AudioManager.PlaySFX(SoundType.StraightShot);
            }

            if (attackTimer >= randTime && randAttack >= 4)
            {
                randTime = Random.Range(1.2f, 1.5f);
                randBullet = Random.Range(0, 5);
                randAttack = Random.Range(1, 6);
                attackTimer = 0;
                AudioManager.PlaySFX(SoundType.WaveShot);
                Instantiate(bullet[randBullet], transform.position, Quaternion.Euler(0, 0, -10f));
                Instantiate(bullet[randBullet], transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(bullet[randBullet], transform.position, Quaternion.Euler(0, 0, 10f));
            }
        }
    }

    void LateUpdate()
    {
        if (animateTimer > 0.3f)
        {
            animator.SetBool("Hit", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BossWalls"))
        {
            dirSpeed = dirSpeed * -1;
        }

        else if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            AudioManager.PlaySFX(SoundType.Damaged);
            animateTimer = 0;
            animator.SetBool("Hit", true);
            bossHealth -= 1;
            scoreScript.ScoreUpdate(5f);
        }
    }

    void OnDisable()
    {
        levelStartScript.savedEnemiesList.Remove(gameObject);
        scoreScript.ScoreUpdate(scoreValue);
        Destroy(gameObject);
    }

    IEnumerator DeathSequence()
    {
        animateTimer = 0;
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
