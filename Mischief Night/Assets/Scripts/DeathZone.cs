using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DeathZone : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] GameObject borderEnemyPrefab;
    [SerializeField] Transform safeSide;

    [Header("Options")]
    [SerializeField] float smoothingRate = 4f;

    GameObject borderEnemy;
    Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        borderEnemy = Instantiate(borderEnemyPrefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        FollowPlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        StopFollowingPlayer();
    }


    Vector3 targetPos;
    bool following;

    private void Update()
    {
        if (following)
        {
            Vector3 towardsDeath = (this.transform.position - safeSide.position).normalized;

            targetPos = player.transform.position + (towardsDeath * 8f);
        }

        borderEnemy.transform.position = Vector3.Lerp(borderEnemy.transform.position, targetPos, smoothingRate * Time.deltaTime);
        borderEnemy.transform.LookAt(player.transform);
        var rot = borderEnemy.transform.rotation.eulerAngles;
        borderEnemy.transform.rotation = Quaternion.Euler(0f, rot.y, rot.z);
    }

    private void FollowPlayer()
    {
        following = true;
    }
    private void StopFollowingPlayer()
    {
        following = false;

        if (Vector3.Distance(player.transform.position, safeSide.position) > Vector3.Distance(player.transform.position, this.transform.position))
        {
            player.Kill();
        }
        else
        {
            targetPos = borderEnemy.transform.position + (Vector3.down * 5f);
        }
    }
}
