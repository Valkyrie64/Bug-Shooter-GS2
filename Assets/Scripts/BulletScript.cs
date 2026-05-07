using System;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] SpriteRenderer rend;

    void Awake()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * (bulletSpeed * Time.deltaTime));
        if (!rend.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FlyingEnemy") || other.CompareTag("StoppedEnemy") || other.CompareTag("StartingEnemy"))
        {
            if (!other.gameObject.name.Contains("Robo"))
            {
                other.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}
