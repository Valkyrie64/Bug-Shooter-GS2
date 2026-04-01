using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    void Start()
    {
        scoreScript = scoreGO.GetComponent<ScoringScript>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += OnMove;
        inputActions.Player.Movement.canceled += OnMove;
        inputActions.Player.Shoot.performed += OnShoot;
        inputActions.Player.Shoot.canceled += OnShoot;
        inputActions.Player.Pause.performed += OnPause;
        inputActions.Player.Pause.canceled += OnPause;
    }

    void OnDisable()
    {
        inputActions.Player.Movement.performed -= OnMove;
        inputActions.Player.Movement.canceled -= OnMove;
        inputActions.Player.Shoot.performed -= OnShoot;
        inputActions.Player.Shoot.canceled -= OnShoot;
        inputActions.Player.Pause.performed -= OnPause;
        inputActions.Player.Pause.canceled -= OnPause;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        Debug.Log("Should Shoot");
    }

    void OnPause(InputAction.CallbackContext context)
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animateTimer += Time.deltaTime;

        if (health <= 0)
        {
            //restartButton.SetActive(true);
            //Destroy(gameObject);
        }
    }

    public void LateUpdate()
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
