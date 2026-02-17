using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] private GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (bulletSpeed * Time.deltaTime));

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
