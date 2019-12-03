using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownSpawn : DimensionedObject
{

    public GameObject[] playerSpawns;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetOverworld() {}

    protected override void SetUpsideDown()
    {
        player.transform.position = playerSpawns[Random.Range(0, playerSpawns.Length)].transform.position;
    }
}
