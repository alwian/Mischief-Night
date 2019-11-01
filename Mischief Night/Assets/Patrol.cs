// Patrol.cs
using UnityEngine;
using UnityEngine.AI;


public class Patrol : MonoBehaviour
{

    public Transform[] points;
    private NavMeshAgent agent;
    private int nextPoint = 0;

    readonly Color[] colors = new Color[5]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue,
        Color.magenta
    };

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    private void GoToNextPoint()
    {
        if (points.Length > 1)
        {
            agent.SetDestination(points[nextPoint].position);
            nextPoint = (nextPoint + 1) % points.Length;
        }
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

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