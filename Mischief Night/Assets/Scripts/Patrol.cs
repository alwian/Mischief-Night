// Patrol.cs
using UnityEngine;
using UnityEngine.AI;


public class Patrol : DimensionedObject
{

    public Transform[] points;
    private NavMeshAgent agent;
    private int nextPoint = 0;
    private bool patrolling = true;
    private bool onPath = true;
    public float searchRadius = 5;

    readonly Color[] colors = new Color[5]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue,
        Color.magenta
    };

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
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
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrolling)
        {

            GoToNextPoint();
            onPath = true;
        }

        if (!patrolling && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
            patrolling = true;


        }

        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (patrolling && onPath)
        {
            foreach (Collider hit in Physics.OverlapSphere(transform.position, searchRadius))
            {
                if (hit.gameObject.tag == "Player")
                {
                    patrolling = false;
                    onPath = false;
                    agent.SetDestination(hit.gameObject.transform.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        if (points.Length >= 2) {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.color = colors[i % colors.Length];
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DimensionManager.Instance.SetDimension(Dimension.UPSIDE_DOWN);
        }
    }

    protected override void SetOverworld()
    {
        gameObject.SetActive(true);
    }

    protected override void SetUpsideDown()
    {
        print("SWITCHED");
        gameObject.SetActive(false);
    }
}