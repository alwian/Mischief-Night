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
        EnableAll();
    }
    private void OnTriggerExit(Collider other)
    {
        DisableAll();
    }
}
