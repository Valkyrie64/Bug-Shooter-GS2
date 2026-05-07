using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject bullet;
    public Transform barrel;
    public int health;
    public GameObject scoreGO;
    private ScoringScript scoreScript;

    //animation components
    public Animator animator;
    public bool currentShotAnim;
    private float animateTimer;
    
    private PlayerInputActions inputActions;

    private bool autoShooting;
    private float timeBeforeShot;
    [SerializeField] private float fireRate;
    
    [SerializeField] private GameObject[] livesImages;
    private Collider2D playerColl;
    private bool gameEnd;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    void Start()
    {
        timeBeforeShot = 1/fireRate;
        scoreScript = scoreGO.GetComponent<ScoringScript>();
        playerColl = gameObject.GetComponent<Collider2D>();
        var autoOn = PlayerPrefs.GetInt("AutoFire");
        if (autoOn == 0)
        {
            Debug.Log("AutoFire disabled");
        }

        if (autoOn == 1)
        {
            Debug.Log("AutoFire enabled");
        }
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += OnMove;
        inputActions.Player.Movement.canceled += OnMove;
        inputActions.Player.Shoot.started += OnShoot;
        inputActions.Player.Shoot.performed += OnShoot;
        inputActions.Player.Shoot.canceled += OnShoot;
        inputActions.Player.Pause.performed += OnPause;
        inputActions.Player.Pause.canceled += OnPause;
    }

    void OnDisable()
    {
        inputActions.Player.Movement.performed -= OnMove;
        inputActions.Player.Movement.canceled -= OnMove;
        inputActions.Player.Shoot.started -= OnShoot;
        inputActions.Player.Shoot.performed -= OnShoot;
        inputActions.Player.Shoot.canceled -= OnShoot;
        inputActions.Player.Pause.performed -= OnPause;
        inputActions.Player.Pause.canceled -= OnPause;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (LevelStartScript.levelStarted)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);
        }
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        if (LevelStartScript.levelStarted)
        {
            var autoFire = PlayerPrefs.GetInt("AutoFire");
            switch (autoFire)
            {
                case 0:
                    if (context.performed)
                    {
                        Instantiate(bullet, barrel.position, Quaternion.identity);
                
                        AudioManager.PlaySFX(SoundType.PlayerShot);
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
                    break;
                case 1:
                    if (context.started)
                    {
                        autoShooting = true;
                    }

                    if (context.canceled)
                    {
                        autoShooting = false;
                    }
                    break;
            }
        }
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

    void GameLose()
    {
        SceneManager.LoadScene(sceneBuildIndex: 11);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (autoShooting)
        {
            if (timeBeforeShot <= 0f)
            {
                Instantiate(bullet, barrel.position, Quaternion.identity);
                
                AudioManager.PlaySFX(SoundType.PlayerShot);
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
                timeBeforeShot = 1/fireRate;
            }
            else
            {
                timeBeforeShot -= Time.deltaTime;
            }
        }
        else
        {
            timeBeforeShot = 0;
        }
    }

    public void LateUpdate()
    {
        if (animateTimer > 0.3f)
        {
            animator.SetBool("LeftWing", false);
            animator.SetBool("RightWing", false);
        }
        
        if (animateTimer > 1.4f)
        {
            if (health > 0)
            {
                animateTimer = 0;
                animator.SetBool("Hit", false);
                playerColl.enabled = true;
                inputActions.Enable();
            }
            else
            {
                GameLose();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("TrackingBullet"))
        {
            playerColl.enabled = false;
            LifeLost();
            scoreScript.timer -= 5f;
            Destroy(other.gameObject);
        }
    }

    void LifeLost()
    {
        animateTimer = 0;
        health--;
        livesImages[health].SetActive(false);
        inputActions.Disable();
        animator.SetBool("Hit", true);
    }
}
