// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : DimensionedObject
{

    public Transform[] points;
    public AudioClip[] patrolSounds;
    public AudioClip[] attackSounds;

    private AudioSource audioSource;
    private NavMeshAgent agent;
    private Animator animator;
    private int nextPoint = 0;
    public float searchRadius = 5;

    private bool chasing;
    private bool attacking;

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
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartAudio());

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;

        animator = GetComponentInChildren<Animator>();
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
        }

        if (chasing && !attacking && agent.remainingDistance < 3.0f)
        {
            StartCoroutine(AttackPlayer());
        }

        DetectPlayer();
    }

    IEnumerator StartAudio()
    {
        while(true)
        {
            if (attacking)
            {
                audioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
            } else
            {
                audioSource.clip = patrolSounds[Random.Range(0, attackSounds.Length)];
            }
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    IEnumerator AttackPlayer()
    {
        attacking = true;
        agent.isStopped = true;
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
}