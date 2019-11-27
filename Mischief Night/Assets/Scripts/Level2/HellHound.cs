/*
 * Authors: Colton Campbell (B00693513)
 *          Amanda... (B00...)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class HellHound : MonoBehaviour
{
    Player target;
    NavMeshAgent agent;
    Animator anim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.Player;

        agent.speed *= Random.Range(1f, 1.25f);

        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
        Animate();
    }

    private void Animate()
    {
        anim.SetBool("Move", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponentInParent<Player>();
        if (player)
        {
            anim.SetBool("Attack", true);
            player.Kill();
        }
    }
}
