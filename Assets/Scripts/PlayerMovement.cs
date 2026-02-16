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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, barrel.position, Quaternion.identity);
        }
        

        if (health <= 0)
        {
            //restartButton.SetActive(true);
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            scoreScript.timer -= 5f;
            Destroy(other.gameObject);
            //health--;
            //Destroy(other.gameObject);
        }
    }
}
