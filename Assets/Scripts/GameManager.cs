using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cell cellPrefab;
    // public GameObject cam;
    // public GameObject finish;
    public int width = 8;
    public int height = 8;

    private void Start()
    {
        width++;

        MazeGenerator generator = new MazeGenerator();
        generator.SetWidthAndHeight(width, height);
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);

                c.wallLeft.SetActive(maze[x, y].wallLeft);
                c.wallBottom.SetActive(maze[x, y].wallBottom);
                c.wallRight.SetActive(maze[x, y].wallRight);
                c.wallTop.SetActive(maze[x, y].wallTop);
            }
        }

        // cam.transform.position = new Vector3(width / 2, height / 2, cam.transform.position.z);
        // finish.transform.position = new Vector2(width - 0.5f, height - 0.5f);
    }
}
