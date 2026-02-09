using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public ScoringScript scoreScript;
    public float scoreValue;
    private float speed = 30f;
    private GameObject rotatePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (rotatePoint != null)
        {
            transform.RotateAround(rotatePoint.transform.position, Vector3.back, speed * Time.deltaTime);
        }*/


        
    }

    private void OnDisable()
    {
        scoreScript.ScoreUpdate(scoreValue);
        Destroy(gameObject);
    }

    /*private void PathCurve()
    {
        rotatePoint = new GameObject();
        rotatePoint.transform.position = transform.position + transform.right;
    }*/
}
