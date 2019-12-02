using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public Collider lockTrigger;
    private Door door;

    void Start()
    {
        door = GetComponent<Door>();
        print(door == null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            print("Lock Doors");
            if (door.doorOpen)
            {
                door.Interact();
            }
            door.locked = true;
        }
    }
}
