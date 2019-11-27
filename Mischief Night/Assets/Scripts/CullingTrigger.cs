/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] cullObjects;

    private void EnableAll()
    {
        foreach (var obj in cullObjects)
            obj.SetActive(true);
    }
    private void DisableAll()
    {
        foreach (var obj in cullObjects)
            obj.SetActive(false);
    }

    private void Awake()
    {
        DisableAll();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EnableAll();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            DisableAll();
    }
}
