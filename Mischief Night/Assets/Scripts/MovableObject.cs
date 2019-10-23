using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : MonoBehaviour, IPhysicsObject
{
    new Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public bool IsPickedUp { get; private set; }

    public void Pickup(Transform newParent)
    {
        rigidbody.isKinematic = true;
        IsPickedUp = true;

        this.transform.parent = newParent;
        this.transform.ResetLocal();
    }
    public void Drop()
    {
        rigidbody.isKinematic = false;
        IsPickedUp = false;
    }
}
