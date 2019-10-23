/*
 * A helper interface. Allows us to skip casting interfaces to Monobehaviours
 * when we need access to Unity functions or the transform.
 */
using UnityEngine;

public interface IGameObject
{
    GameObject gameObject { get; }
    Transform transform { get; }
}
