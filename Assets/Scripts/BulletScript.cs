using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] SpriteRenderer rend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
