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
    public int scoreNumber;
    public GameObject scoreGO;
    public GameObject livesGO;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject restartButton;

    void Start()
    {
        //scoreText = scoreGO.GetComponent<TextMeshProUGUI>();
        //livesText = livesGO.GetComponent<TextMeshProUGUI>();
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
        
        //scoreText.text = $"Score: {scoreNumber}";
        //livesText.text = $"Lives: {health}";

        if (health <= 0)
        {
            //restartButton.SetActive(true);
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            //health--;
            //Destroy(other.gameObject);
        }
    }
}
