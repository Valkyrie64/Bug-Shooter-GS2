using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public ScoringScript scoreScript;
    public float scoreValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        scoreScript.ScoreUpdate(scoreValue);
        Destroy(gameObject);
    }
}
