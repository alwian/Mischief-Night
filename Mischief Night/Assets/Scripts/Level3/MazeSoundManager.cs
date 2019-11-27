/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundEmitter;
    [SerializeField] float speedMultiplier = 5f;
    [SerializeField] PathFinder pather;

    Player player;

    Node activeNode;
    Node targetNode;
    List<Node> path;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }

    // Update the path
    public void UpdateNode(Node node)
    {
        activeNode = node;
        path = pather.GeneratePath(activeNode, targetNode);
    }

    public void SetTarget(Node node)
    {
        targetNode = node;
        path = pather.GeneratePath(activeNode, targetNode);
    }

    // Use the path and current position to place the sound
    private void Update()
    {
        if (path == null || !targetNode || !activeNode)
        {
            soundEmitter.SetActive(false);
            return;
        }

        Vector3 targetPos;
        if (path.Count < 3)
            targetPos = path[path.Count - 1].transform.position;
        else
        {

            var pos1 = path[0].transform.position;
            var pos2 = path[1].transform.position;
            var pos3 = path[2].transform.position;

            float full = Vector3.Distance(pos1, pos2);
            float players = Vector3.Distance(player.transform.position, pos2);



            float val = (full - players) / full;
            val += 0.5f;

            if (val > 1.0)
                targetPos = Vector3.Lerp(pos2, pos3, val - 1.0f);
            else
                targetPos = Vector3.Lerp(pos1, pos2, val);


        }
        soundEmitter.SetActive(true);
        soundEmitter.transform.position = Vector3.Lerp(soundEmitter.transform.position, targetPos, Time.deltaTime * speedMultiplier);
    }

    private static int FindClosest(Vector3 toPos, Vector3[] positions)
    {
        int closest = -1;
        float lowestDist = float.MaxValue;

        for(int i=0; i<positions.Length; i++)
        {
            var pos = positions[i];

            var dist = Vector3.Distance(toPos, pos);
            if (dist < lowestDist)
            {
                lowestDist = dist;
                closest = i;
            }
        }

        return closest;
    }
}
