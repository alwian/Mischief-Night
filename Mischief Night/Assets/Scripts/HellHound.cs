using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HellHound : MonoBehaviour
{
    Player target;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.Player;
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
