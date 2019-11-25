using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// The Node used by the pathfinding alogorithm for Level 3's maze
/// Includes MenuItem and Gizmo code to speed up development
/// Also has Undo support
/// </summary>
public class Node : MonoBehaviour
{
    public List<Node> connections = new List<Node>();

    static Node activeNode;
    static readonly string UNDO_NAME = "Node Connections";

    [MenuItem("Nodes/Select #1")]
    static void SelectNode()
    {
        if (Selection.transforms.Length != 1)
        {
            Debug.Log("No node selected. Please select a single node from the scene.");
            return;
        }

        activeNode = Selection.transforms[0].GetComponent<Node>();
    }

    [MenuItem("Nodes/Connect #2")]
    static void ConnectNodes()
    {
        if (!activeNode)
        {
            Debug.Log("Please select a node first, using the Select option.");
            return;
        }

        List<Node> nodes = new List<Node>();
        foreach (var transform in Selection.transforms)
        {
            var node = transform.GetComponent<Node>();
            if (!node)
                continue;
            else
                nodes.Add(node);
        }


        if (nodes.Count > 0)
        {
            Undo.RegisterCompleteObjectUndo(activeNode, UNDO_NAME);

            // Connect the nodes to the activeNode
            for (int i=0; i<nodes.Count; i++)
            {
                Node newNode = nodes[i];
                if (activeNode == newNode)
                    continue;

                Undo.RegisterCompleteObjectUndo(newNode, UNDO_NAME);

                if (activeNode.connections.Contains(newNode) == false)
                    activeNode.connections.Add(newNode);
                
                if (newNode.connections.Contains(activeNode) == false)
                    newNode.connections.Add(activeNode);

                EditorUtility.SetDirty(newNode);
            }

            EditorUtility.SetDirty(activeNode);
        }
    }

    [MenuItem("Nodes/Clear Connections #3")]
    static void ClearConnections()
    {
        List<Node> nodes = new List<Node>();
        foreach (var transform in Selection.transforms)
        {
            var node = transform.GetComponent<Node>();
            if (!node)
                continue;
            else
                nodes.Add(node);
        }

        if (nodes.Count > 0)
        {
            // Connect the nodes to the activeNode
            for (int i = 0; i < nodes.Count; i++)
            {
                Node newNode = nodes[i];
                if (activeNode == newNode)
                    continue;

                Undo.RegisterCompleteObjectUndo(newNode, UNDO_NAME);
                newNode.connections.Clear();
                EditorUtility.SetDirty(newNode);
            }
        }
    }

    // Visualizes the connections between nodes
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var connection in connections)
        {
            // Calculate an offset to the lines right
            // Makes it easier to see one-way vs two-way connections
            Vector3 dir = (connection.transform.position - this.transform.position).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, dir) * 0.5f;

            Gizmos.DrawLine(this.transform.position + right, connection.transform.position + right);
        }
    }
}
