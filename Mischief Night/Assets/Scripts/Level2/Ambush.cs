using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    [Header("Required Reference")]
    [SerializeField] Collectable trigger;
    [SerializeField] GameObject enemyPrefab;
    [Space]
    [SerializeField] Transform[] spawnPoints;


    private void Awake()
    {
        trigger.OnCollect += TriggerAmbush;
    }

    private void TriggerAmbush(Collectable c)
    {
        foreach (var sp in spawnPoints)
            Instantiate(enemyPrefab, sp.position, sp.rotation);
    }
}
