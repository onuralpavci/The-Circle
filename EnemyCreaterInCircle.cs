using UnityEngine;

[System.Serializable]
public class EnemyCreaterInCircle
{
    public Vector2 circleAreaPosition;
    public float radius;
    public int numberOfEnemies;
    public Vector2[] enemyPositions;

    public float randomAngle;

    public void CreatePositions()
    {
        enemyPositions = new Vector2[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            randomAngle = Random.Range(0f, 1f) * 2 * Mathf.PI;
            enemyPositions[i] = new Vector2( radius * Mathf.Sqrt( Random.Range(0f , 1f)) * Mathf.Cos(randomAngle) , radius * Mathf.Sqrt(Random.Range(0f, 1f) ) * Mathf.Sin(randomAngle) ) + circleAreaPosition;
        }
    }

}
