using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public Door[] doorsToLock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Door door in doorsToLock)
            {
                if (door.doorOpen)
                {
                    door.Interact(); 
                }
                door.locked = true;
            }
        }
    }
}
