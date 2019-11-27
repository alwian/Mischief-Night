/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsObject : IGameObject
{
    bool IsPickedUp { get; }

    void Pickup(Transform newParent);
    void Drop();
}
