// Patrol.cs
using UnityEngine;


public class Patrol : MonoBehaviour
{

    public Transform[] points;
    readonly Color[] colors = new Color[5]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue,
        Color.magenta
    };

    private void OnDrawGizmos()
    {
        if (points.Length >= 2) {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.color = colors[i % colors.Length];
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
        }
    }
        
    
}