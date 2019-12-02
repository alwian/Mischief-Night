using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Deathbringer : MonoBehaviour
{
    [SerializeField] string endGameScene;

    NavMeshAgent agent;
    Player player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(endGameScene);
    }
}
