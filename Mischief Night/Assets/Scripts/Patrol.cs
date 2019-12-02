// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/*
* Author: Alex Anderson
* 
* Edited by: Amanda Norman
*/

public class Patrol : DimensionedObject
{
    public Transform[] points;
    private NavMeshAgent agent;
    private Animator animator;
    private int nextPoint = 0;
    public float searchRadius = 5;

    private bool chasing;
    private bool attacking;

    private enum AnimateState //Enumerator variables made to create animation states for audio use
    {
        Walk = 0,
        Run = 1,
        Attack = 2
    }

    private AnimateState state;

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

        animator = GetComponentInChildren<Animator>();
        state = AnimateState.Walk;
        animator.SetFloat("speed", 1);
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
        if (!chasing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
            state = AnimateState.Walk;
        }

        if (chasing && !attacking && agent.remainingDistance < 3.0f)
        {
            StartCoroutine(AttackPlayer());
        }

        DetectPlayer();
    }

    IEnumerator AttackPlayer()
    {
        attacking = true;
        agent.isStopped = true;
        state = AnimateState.Attack;
        animator.SetBool("attack", true);
        yield return null;
        animator.SetBool("attack", false);
        animator.SetFloat("speed", 1f);
        agent.isStopped = false;
        attacking = false;
    }

    private void DetectPlayer()
    {
        if (!attacking)
        {
            foreach (Collider hit in Physics.OverlapSphere(transform.position, searchRadius))
            {
                if (hit.gameObject.tag == "Player")
                {
                    chasing = true;
                    animator.SetFloat("speed", 2f);
                    //state = AnimateState.Run;
                    agent.SetDestination(hit.gameObject.transform.position);
                    return;
                }
            }
        }
        

        animator.SetFloat("speed", 1f);
        chasing = false;
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
        gameObject.SetActive(false);
    }

    public string GetState()
    {
        return state.ToString();
    }
}