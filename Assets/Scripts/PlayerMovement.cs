using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject bullet;
    public Transform barrel;
    public int health;
    public GameObject scoreGO;
    private ScoringScript scoreScript;
    public GameObject livesGO;
    public GameObject restartButton;

    //animation components
    public Animator animator;
    public bool currentShotAnim;
    private float animateTimer;

    public AudioSource sfxSource;

    void Start()
    {
        scoreScript = scoreGO.GetComponent<ScoringScript>();
    }
    // Update is called once per frame
    void Update()
    {
        var moveH = Input.GetAxisRaw("Horizontal");
        var moveV = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector3(moveH * speed, moveV * speed, 0);

        animateTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, barrel.position, Quaternion.identity);
            sfxSource.Play();
            //animation
            animateTimer = 0;
            switch (currentShotAnim)
            {
                case true:
                    animator.SetBool("LeftWing", true);
                    animator.SetBool("RightWing", false);
                    currentShotAnim = false;
                    break;
                case false:
                    animator.SetBool("RightWing", true);
                    animator.SetBool("LeftWing", false);
                    currentShotAnim = true;
                    break;
            }

        }

        if (health <= 0)
        {
            //restartButton.SetActive(true);
            //Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (animateTimer > 0.3f)
        {
            animator.SetBool("LeftWing", false);
            animator.SetBool("RightWing", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("TrackingBullet"))
        {
            scoreScript.timer -= 5f;
            Destroy(other.gameObject);
            //health--;
            //Destroy(other.gameObject);
        }
    }
}
