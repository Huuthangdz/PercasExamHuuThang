using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FindWayScript : MonoBehaviour
{
    public GenerateMapScript generateMapScript;
    private List<Vector2Int> path = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        if (generateMapScript == null)
        {
            Debug.LogError("GenerateMapScript");
        }

        if (generateMapScript.grid == null)
        {
            Debug.LogError("Grid");
        }
        FindWay();
        VisualizePath();
    }

    private void FindWay()
    {
        int[,] grid = generateMapScript.grid;
        Vector2Int start = generateMapScript.start;
        Vector2Int end = generateMapScript.Goal;

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        queue.Enqueue(start);
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            if (current == end)
            {
                while (current != start)
                {
                    if(current != end)
                    {
                        path.Add(current);
                    }
                    current = cameFrom[current];
                }
                path.Reverse();
                return;
            }     
            foreach(Vector2Int neighbor in GetNeighbors(current, grid))
            { 
                if (!cameFrom.ContainsKey(neighbor) && grid[neighbor.x, neighbor.y] == 0)
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
    }

    List<Vector2Int> GetNeighbors(Vector2Int position, int[,] grid)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach(Vector2Int dr in directions)
        {
            Vector2Int neighbor = position + dr;
            if (IsPositionValid(neighbor,grid))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }
    private bool IsPositionValid(Vector2Int position, int[,] grid)
    {
        return position.x >= 0 && position.x < generateMapScript.Rows &&
               position.y >= 0 && position.y < generateMapScript.Collums;
    }
    private void VisualizePath()
    {
        if (generateMapScript.Parrent == null)
        {
            Debug.LogError("Parrent");
            return;
        }
        foreach(Vector2Int pos in path)
        {
            int index = pos.x * generateMapScript.Collums + pos.y;
            if(index >= 0 && index < generateMapScript.Parrent.childCount)
            {
                Transform cell = generateMapScript.Parrent.GetChild(index);
                SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = Color.yellow;
                }
            }
        }
    }
}
