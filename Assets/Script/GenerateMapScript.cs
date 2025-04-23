using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapScript : MonoBehaviour
{
    public int Rows = 10;
    public int Collums = 10;

    public GameObject CellPrefabs;
    public Transform Parrent;
    public Vector2Int start = new Vector2Int(0, 0);
    public Vector2Int Goal;

    [HideInInspector] public int[,] grid;

    void Start()
    {
        Goal = new Vector2Int(Rows - 1, Collums - 1);

        Goal.x = Mathf.Clamp(Goal.x, 0, Rows - 1);
        Goal.y = Mathf.Clamp(Goal.y, 0, Collums - 1);

        GenerateMap();
        VisualizeGrid();
        FixCamera();
    }

    public void GenerateMap()
    {
        grid = new int[Rows, Collums];
        for ( int i = 0;i < Rows; i++)
        {
            for (int j = 0; j < Collums; j++)
            {
                grid[i, j] = Random.Range(0, 100) < 30 ? 1 : 0;
            }
        }
        if (start.x >= 0 && start.x < Rows && start.y >= 0 && start.y < Collums)
        {
            grid[start.x, start.y] = 0;
        }
        else
        {
            Debug.LogError($"Start position {start} is out of bounds!");
        }

        if (Goal.x >= 0 && Goal.x < Rows && Goal.y >= 0 && Goal.y < Collums)
        {
            grid[Goal.x, Goal.y] = 0;
        }
        else
        {
            Debug.LogError($"Goal position {Goal} is out of bounds!");
        }
    }

    public void VisualizeGrid()
    {
        for(int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Collums; j++)
            {
                GameObject cell = Instantiate(CellPrefabs, new Vector3(i, j, 0), Quaternion.identity, Parrent);
                SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
                if (grid[i, j] == 1)
                {
                    spriteRenderer.color = Color.gray;
                }
                else if ( new Vector2Int(i, j) == start)
                {
                    spriteRenderer.color = Color.red;
                }
                else if ( new Vector2Int(i, j) == Goal)
                {
                    spriteRenderer.color = Color.green;
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }
    private void FixCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 centerPoint = new Vector3((Rows-1) / 2f, (Collums-1) / 2f, -10);
            mainCamera.transform.position = centerPoint;

            float aspectRatio = (float)Screen.width / Screen.height;
            mainCamera.orthographicSize = Mathf.Max(Rows/2f, Collums / (2f * aspectRatio));
        }
    }
}
