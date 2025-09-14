using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinder
{
    public static List<Waypoint> FindPath(Waypoint start, Waypoint goal)
    {
        // A list of nodes to explore
        var openSet = new List<Waypoint> { start };

        // For tracing the path back
        var cameFrom = new Dictionary<Waypoint, Waypoint>();

        // Cost from start to this node
        var gScore = new Dictionary<Waypoint, float>();

        // Estimated total cost (g + h)
        var fScore = new Dictionary<Waypoint, float>();

        // Initialize scores
        foreach (var waypoint in Object.FindObjectsOfType<Waypoint>())
        {
            gScore[waypoint] = float.MaxValue;
            fScore[waypoint] = float.MaxValue;
        }

        gScore[start] = 0;
        fScore[start] = Vector2.Distance(start.transform.position, goal.transform.position);

        while (openSet.Count > 0)
        {
            // Get node with the lowest fScore
            Waypoint current = openSet[0];
            foreach (var node in openSet)
            {
                if (fScore[node] < fScore[current])
                    current = node;
            }

            // Path complete
            if (current == goal)
            {
                var path = new List<Waypoint>();
                while (cameFrom.ContainsKey(current))
                {
                    path.Insert(0, current);
                    current = cameFrom[current];
                }
                path.Insert(0, start);
                return path;
            }

            openSet.Remove(current);

            foreach (var neighbor in current.neighbors)
            {
                float tentativeG = gScore[current] + Vector2.Distance(current.transform.position, neighbor.transform.position);

                if (tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Vector2.Distance(neighbor.transform.position, goal.transform.position);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }
}
