using UnityEngine;
using System.Linq;

public class InitInverted2DEdges : MonoBehaviour
{

    public int NumEdges;
    public float Radius;

    // Use this for initialization
    void Start()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[NumEdges + 1];

        for (int i = 0; i < NumEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / NumEdges;
            float x = Radius * Mathf.Cos(angle) - .5f;
            float y = Radius * Mathf.Sin(angle) + 10;

            points[i] = new Vector2(x + 1, y);
        }
        points[NumEdges] = points[0];
         
         edgeCollider.points = edgeCollider.points.Concat(points).ToArray();
     }
 }