using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemyManagerScriptableObject : ScriptableObject
{
    [SerializeField] public EnemyType enemyType;
    [SerializeField] public float hitPoints;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float pointsValue;
    [SerializeField] public int attackPattern;
    [SerializeField] public List<Sprite> enemySprites;
    public enum EnemyType
    {
        Black_Ant,
        Red_Ant,
        Yellow_Ant,
        Soldier_Ant,
        Ant_Genral,
        Ant_Kamikaze
    }

    public void SetEnemyType(int waveNO)
    {
        switch (waveNO)
        {
            case 0:
                enemyType = EnemyType.Black_Ant;
                hitPoints = 15f;
                attackSpeed = 2f;
                pointsValue = 10f;
                attackPattern = 1;
                break;
            case 1:
                enemyType = EnemyType.Red_Ant;
                hitPoints = 15f;
                attackSpeed = 1f;
                pointsValue = 15f;
                attackPattern = 2;
                break;
            case 2:
                enemyType = EnemyType.Yellow_Ant;
                hitPoints = 25f;
                attackSpeed = 1f;
                pointsValue = 30f;
                break;
            case 3:
                enemyType = EnemyType.Soldier_Ant;
                hitPoints = 45f;
                attackSpeed = 2f;
                pointsValue = 50f;
                break;
            case 4:
                enemyType = EnemyType.Ant_Genral;
                hitPoints = 65f;
                attackSpeed = 2f;
                pointsValue = 70f;
                break;
            case 5:
                enemyType = EnemyType.Ant_Kamikaze;
                hitPoints = 5f;
                attackSpeed = 1f;
                pointsValue = 100f;
                break;
        }
    }
}
