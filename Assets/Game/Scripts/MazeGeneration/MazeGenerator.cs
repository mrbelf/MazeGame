using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] bool GenerateOnAwake;
    [SerializeField] List<Cell> maze;
    public static readonly float CellSize = 1f;
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 10;
    [SerializeField] GameObject rightWallPrefab;
    [SerializeField] GameObject topWallPrefab;


    public Vector2Int Dimentions => new Vector2Int(rows, columns);

    private void Awake()
    {
        if (GenerateOnAwake) 
        {
            GenerateMaze();

            //var maze_str = JsonUtility.ToJson(new MazeWrapper(maze.ToArray()));
            //
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var newDirectory = Directory.CreateDirectory(docPath + Path.DirectorySeparatorChar);
            //string filePath = Path.Combine(newDirectory.FullName, $"{"maze"}.txt");
            //Debug.Log("Saving maze at : " + filePath);
            //using (StreamWriter outputFile = new StreamWriter(filePath, false))
            //{
            //    outputFile.Write(maze_str);
            //}

            BuildMaze(maze);
        }

    }

    public void BuildMaze(List<Cell> maze) 
    {
        if (maze != null)
        {
            foreach (Cell c in maze)
            {
                c.BuildWalls(topWallPrefab,rightWallPrefab);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(rows*CellSize/2,columns*CellSize/2),new Vector3(rows*CellSize,columns*CellSize));
        if (maze != null) 
        {
            foreach (Cell c in maze) 
            {
                c.ShowGizmos();
            }
        }
    }

    private int GetIndex(int i, int j) 
    {
        return i + j * columns;
    }

    private List<Cell> GetUnvisitedNeighbours(Cell c) 
    {
        List<Cell> neighbours = new List<Cell>();

        if (c.i < rows - 1 && !maze[GetIndex(c.i + 1, c.j)].visited)
            neighbours.Add(maze[GetIndex(c.i + 1, c.j)]);

        if(c.i > 0 && !maze[GetIndex(c.i - 1, c.j)].visited)
            neighbours.Add(maze[GetIndex(c.i - 1, c.j)]);

        if(c.j < columns - 1 && !maze[GetIndex(c.i, c.j + 1)].visited)
            neighbours.Add(maze[GetIndex(c.i, c.j + 1)]);

        if(c.j > 0 && !maze[GetIndex(c.i, c.j - 1)].visited)
            neighbours.Add(maze[GetIndex(c.i, c.j - 1)]);

        return neighbours;
    }

    private void RemoveWall(Cell a, Cell b) 
    {
        if (a.i == b.i)
        {
            if (a.j > b.j)
            {
                b.topWall = false;
            }
            else
            {
                a.topWall = false;
            }
        }
        else if (a.j == b.j)
        {
            if (a.i > b.i)
            {
                b.rightWall = false;
            }
            else
            {
                a.rightWall = false;
            }
        }
        else 
        {
            Debug.LogError(string.Format("To remove a wall cells 'a' and 'b' must be neighbours\na.i: {0}\na.j: {1}\nb.i: {2}\nb.j: {3}",a.i,a.j,b.i,b.j));
        }
    }

    public List<Cell> GenerateMaze() 
    {
        maze = new List<Cell>();
        for (int j = 0; j < rows; j++) 
        {
            for (int i = 0; i < columns; i++) 
            {
                var cell = new Cell(i, j);
                maze.Add(cell);
            }
        }

        Cell current = maze[0];
        current.visited = true;

        int visitedCells = 1;
        Stack<Cell> visitStack = new Stack<Cell>();

        while (visitedCells != maze.Count) 
        {
            if (GetUnvisitedNeighbours(current).Count > 0)
            {
                var neighbs = GetUnvisitedNeighbours(current);

                var next = neighbs[UnityEngine.Random.Range(0, neighbs.Count)];

                visitStack.Push(current);

                RemoveWall(current, next);

                current = next;
                current.visited = true;

                visitedCells++;
            }
            else 
            {
                current = visitStack.Pop();
            }
        }

        return maze;
    }

    [Serializable]
    public class MazeWrapper
    {
        public Cell[] maze;
        public MazeWrapper(Cell [] maze) { this.maze = maze; }
    }

    [Serializable]
    public class Cell
    {
        private float cellSize;
        public int i;
        public int j;
        public bool visited = false;
        public bool rightWall = true;
        public bool topWall = true;

        public Cell(int i, int j) 
        {
            this.cellSize = MazeGenerator.CellSize;
            this.i = i;
            this.j = j;
        }

        public void ShowGizmos() 
        {
            var x = this.i * cellSize;
            var y = this.j * cellSize;

            if (rightWall) 
            {
                Gizmos.DrawLine(new Vector3(x+cellSize,y),new Vector3(x+cellSize,y+cellSize));
            }

            if (topWall) 
            {
                Gizmos.DrawLine(new Vector3(x,y+cellSize),new Vector3(x+cellSize,y+cellSize));
            }
        }

        public void BuildWalls(GameObject topWallPrefab, GameObject rightWallPrefab) 
        {
            var x = this.i * cellSize;
            var y = this.j * cellSize;

            if (rightWall) 
            {
                var rightWall = Instantiate(rightWallPrefab);
                rightWall.transform.position = new Vector3(x + cellSize, y + (cellSize/2));
            }

            if (topWall) 
            {
                var topWall = Instantiate(topWallPrefab);
                topWall.transform.position = new Vector3(x + (cellSize/2), y + cellSize);
            }

            if (this.i == 0) 
            {
                var leftWall = Instantiate(rightWallPrefab);
                leftWall.transform.position = new Vector3(x, y + (cellSize / 2));
            }

            if (this.j == 0)
            {
                var bottomWall = Instantiate(topWallPrefab);
                bottomWall.transform.position = new Vector3(x + (cellSize / 2), y);
            }
        }

    }
}
