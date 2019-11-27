/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] GameObject finalDoor;
    [SerializeField] List<StonePedestal> pedestals = new List<StonePedestal>();
    [SerializeField] MazeSoundManager manager;


    private void Awake()
    {
        foreach (var pedestal in pedestals)
            pedestal.OnPlace += OnPlacement;

        ActivateRandomPedestal();
    }

    private void ActivateRandomPedestal()
    {
        if (pedestals.Count == 0)
            return;

        int index = Random.Range(0, pedestals.Count);
        manager.SetTarget(pedestals[index].Node);
    }

    private void OnPlacement(StonePedestal p)
    {
        pedestals.Remove(p);

        if (pedestals.Count <= 0)
            OpenFinalDoor();
        else
            ActivateRandomPedestal();
    }

    private void OpenFinalDoor()
    {
        finalDoor.SetActive(false);
    }
}
