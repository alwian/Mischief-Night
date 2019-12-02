using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : DimensionedObject, IExaminable
{
    public GameObject[] potentialSpawns;
    public Door[] doorsToOpen;
    void Start()
    {
        gameObject.SetActive(false);
        print(potentialSpawns.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetExamine()
    {
        foreach (Door door in doorsToOpen)
        {
            door.locked = false;
            door.Interact();
            door.locked = true;
        }
        DimensionManager.Instance.SetDimension(Dimension.OVERWORLD);
        return "January 23rd: It's in the woods, behind the school, I don't know what it is, but it must be destroyed, it's the only way.";
    }

    protected override void SetOverworld()
    {
        gameObject.SetActive(false);
    }

    protected override void SetUpsideDown()
    {
        int spawn = Random.Range(0, potentialSpawns.Length);
        transform.position = potentialSpawns[spawn].transform.position;
        gameObject.SetActive(true);
    }
}
