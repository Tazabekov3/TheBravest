using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool wallLeft = true;
    public bool wallBottom = true;
    public bool wallRight = true;
    public bool wallTop = true;

    public bool visited = false;
    public int DistanceFromStart;
}

public class MazeGenerator
{
    public int width = 5;
    public int height = 5;

    public void SetWidthAndHeight(int w, int h)
    {
        this.width = w;
        this.height = h;
    }

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[width, height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        // for (int x = 0; x < maze.GetLength(0); x++)
        // {
        //     maze[x, height - 1].wallLeft = false;
        // }

        // for (int y = 0; y < maze.GetLength(1); y++)
        // {
        //     maze[width - 1, y].wallBottom = false;
        // }

        RemoveWallsWithBacktracker(maze);

        //PlaceMazeExit(maze);

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < width - 1 && !maze[x + 1, y].visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < height - 1 && !maze[x, y + 1].visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) 
            {
                a.wallBottom = false;
                b.wallTop = false;
            }
            else 
            {
                b.wallBottom = false;
                a.wallTop = false;
            }
        }
        else
        {
            if (a.X > b.X) 
            {
                a.wallLeft = false;
                b.wallRight = false;
            }
            else 
            {
                b.wallLeft = false;
                a.wallRight = false;
            }
        }
    }

    // private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    // {
    //     MazeGeneratorCell furthest = maze[0, 0];

    //     for (int x = 0; x < maze.GetLength(0); x++)
    //     {
    //         if (maze[x, height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, height - 2];
    //         if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
    //     }

    //     for (int y = 0; y < maze.GetLength(1); y++)
    //     {
    //         if (maze[width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[width - 2, y];
    //         if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
    //     }

    //     if (furthest.X == 0) furthest.wallLeft = false;
    //     else if (furthest.Y == 0) furthest.wallBottom = false;
    //     else if (furthest.X == width - 2) maze[furthest.X + 1, furthest.Y].wallLeft = false;
    //     else if (furthest.Y == height - 2) maze[furthest.X, furthest.Y + 1].wallBottom = false;
    // }
}