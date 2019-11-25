using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<Node> path;
    
    public List<Node> GeneratePath(Node start, Node end)
    {
        if (!start || !end)
            return null;

        return PathFind(start, end);
    }

    class PathNode
    {
        public PathNode(Node node, PathNode parent, Node dest, float gcost)
        {
            this.node = node;
            this.parent = parent;

            this.gcost = gcost;

            this.hcost = Vector3.Distance(node.transform.position, dest.transform.position);
        }

        public Node node;
        public PathNode parent;
        public float gcost;
        public float hcost;

        public float fcost { get { return gcost + hcost; } }

        public override bool Equals(object obj)
        {
            if (!(obj is PathNode))
                return false;

            var other = (PathNode)obj;


            return this.node == other.node;
        }
    }

    private static List<Node> PathFind(Node start, Node end)
    {
        List<Node> path = new List<Node>();

        List<PathNode> openNodes = new List<PathNode>();
        List<PathNode> closedNodes = new List<PathNode>();

        openNodes.Add(new PathNode(start, null, end, 0f));

        // Until we are done...
        while (openNodes.Count > 0)
        {
            // Get the lowest cost node
            PathNode current = GetLowestCost(openNodes);

            // Return path if at the end
            if (current.node == end)
            {
                while (current != null)
                {
                    path.Add(current.node);
                    current = current.parent;
                }
                path.Reverse();
                return path;
            }

            // Add connection nodes
            foreach(var connectionNode in current.node.connections)
            {
                // Only add unclosed nodes
                if (closedNodes.Contains(current) == false)
                {
                    // Calculate new gcost
                    float newGCost = current.gcost + Vector3.Distance(current.node.transform.position, connectionNode.transform.position);
                    openNodes.Add(new PathNode(connectionNode, current, end, newGCost));
                }
            }
            // Close the current node
            openNodes.Remove(current);
            closedNodes.Add(current);
        }
        return null;
    }
 
    private static PathNode GetLowestCost(List<PathNode> nodes)
    {
        PathNode lowest = nodes[0];
        foreach(var node in nodes)
        {
            if (node.fcost < lowest.fcost)
                lowest = node;
        }
        return lowest;
    }


    private void OnDrawGizmos()
    {
        if (path == null)
            return;

        float size = 0.75f;
        Vector3 offset = Vector3.up;

        for (int i=0; i<path.Count; i++)
        {
            Gizmos.color = Color.blue;
            if (i == 0)
                Gizmos.color = Color.green;
            else if (i == path.Count - 1)
                Gizmos.color = Color.red;

            var node = path[i];
            Gizmos.DrawSphere(node.transform.position + offset, size);
        }
    }
}