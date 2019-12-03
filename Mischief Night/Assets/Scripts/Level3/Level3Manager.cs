/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : LevelManager
{
    [Header("Required References")]
    [SerializeField] GameObject finalDoor;
    [SerializeField] List<StonePedestal> pedestals = new List<StonePedestal>();
    [SerializeField] MazeSoundManager manager;
    [SerializeField] GameObject deathbringerPrefab;
    [SerializeField] Transform deathbringerSpawn;
    [SerializeField] Lantern lantern;
    [SerializeField] AudioSource panicAudio;

    [Header("Objectives")]
    [SerializeField] string startObjective;
    [SerializeField] string panicObjective;
    [SerializeField] string endObjective;

    [Header("Time Limit")]
    [SerializeField] float timeLimit = 180f;

    bool levelStarted = false;
    float timeLeft;

    public override void StartLevel()
    {
        base.StartLevel();

        var CM = CameraManager.Instance;
        CM.SetFade(1.0f);
        CM.FadeIn(1.5f);

        GameManager.Instance.Player.SetObjective(startObjective);
        levelStarted = true;
    }

    private void Awake()
    {
        foreach (var pedestal in pedestals)
            pedestal.OnPlace += OnPlacement;

        ActivateRandomPedestal();
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (!levelStarted)
            return;

        timeLeft -= Time.deltaTime;
        lantern.SetOilLevel(timeLeft / timeLimit);

        if (timeLeft <= 0)
        {
            ActivateDeath();
        }
    }

    bool deathActivated = false;
    private void ActivateDeath()
    {
        if (deathActivated)
            return;

        panicAudio.Play();
        GameManager.Instance.Player.SetObjective(panicObjective);

        Instantiate(deathbringerPrefab, deathbringerSpawn.position, deathbringerSpawn.rotation);
        deathActivated = true;
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
        GameManager.Instance.Player.SetObjective(endObjective);
    }
}
