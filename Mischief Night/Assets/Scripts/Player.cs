/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] float respawnDelay;

    PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    bool isDead = false;
    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;
        controller.enablePlayerControl = false;
        controller.DropCamera();
        StartCoroutine(DelayedReload());
    }

    IEnumerator DelayedReload()
    {
        yield return new WaitForSeconds(respawnDelay);
        GameManager.Instance.Reload();
    }

}
