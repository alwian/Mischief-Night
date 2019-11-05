using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player { get; private set; }

    [SerializeField] Player playerPrefab;
    [SerializeField] Transform spawnPoint;

    public GameManager()
    {
        if (!Instance)
            Instance = this;
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Debug.Log("Destroying duplicate GameManager...");
            Destroy(this);
        }
        Player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
