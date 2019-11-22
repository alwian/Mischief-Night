using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{
    [SerializeField] MazeSoundManager manager;
    
    // Change nodes on touch 
    private void OnTriggerEnter(Collider other)
    {
        var node = other.GetComponentInParent<Node>();
        if (node)
        {
            manager.UpdateNode(node);
        }
    }
}
