using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerPos = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameObject.tag)
        {
            case "EnemyBullet":
                transform.Translate(Vector3.down * (bulletSpeed * Time.deltaTime));
                break;
            case "TrackingBullet":
                transform.Translate((-playerPos / 10) * (bulletSpeed * Time.deltaTime));
                break;
        }

        if (!rend.isVisible)
        {
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Hit");
    //        Destroy(gameObject);
    //    }
    //}
}
