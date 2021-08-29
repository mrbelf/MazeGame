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
    [SerializeField] public int rows = 10;
    [SerializeField] public int columns = 10;
    [SerializeField] GameObject rightWallPrefab;
    [SerializeField] GameObject rightWallPrefabTorch;
    [SerializeField] GameObject rightWallPrefabMoving;
    [SerializeField] GameObject topWallPrefab;
    [SerializeField] GameObject topWallPrefabTorch;
    [SerializeField] GameObject topWallPrefabMoving;
    [SerializeField] GameObject pillar;
    private int[,] map; //1 = torch, 2 = door, 3 = others

    public Vector2Int Dimentions => new Vector2Int(rows, columns);

    private void Awake()
    {
        map = new int[rows, columns];
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
                c.BuildWalls(topWallPrefab, topWallPrefabTorch, topWallPrefabMoving, rightWallPrefab, rightWallPrefabTorch, rightWallPrefabMoving, map, rows, columns);
            }
            if(pillar)
                BuildPillars();
        }
    }

    private void BuildPillars() 
    {
        for (int i = 0; i < rows + 1; i++)
            for (int j = 0; j < columns + 1; j++)
                Instantiate(pillar,new Vector3(i*CellSize,j*CellSize),pillar.transform.rotation,null);
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
        for (int j = 0; j < columns; j++) 
        {
            for (int i = 0; i < rows; i++) 
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

    public Vector3 RandomPositionInMazeBoundary()
    {
        int x=0;
        int y=0;
        bool found = false;
        int c = 0;
        while(found == false && c<(rows*columns))
        {
            c++;
            var tmp1 = UnityEngine.Random.Range(0f, 1f);
            if(tmp1 < 0.5)
            {
                var tmp2 = UnityEngine.Random.Range(0f, 1f);
                if (tmp2 < 0.5)
                {
                    x = 0;
                }
                else
                {
                    x = rows - 1;
                }
                y = UnityEngine.Random.Range(0, columns);
            }
            else
            {
                var tmp2 = UnityEngine.Random.Range(0f, 1f);
                if (tmp2 < 0.5)
                {
                    y = 0;
                }
                else
                {
                    y = columns - 1;
                }
                x = UnityEngine.Random.Range(0, rows);
            }
            if(map[x,y] == 0 && (x != 0 || y != 0))
            {
                found = true;
                map[x, y] = 2;
            }

        }
        return new Vector3(x * CellSize + CellSize / 2, y * CellSize + CellSize / 2);
    }

    public Vector3 RandomPositionInMaze()
    {
        int x = 0;
        int y = 0;
        bool found = false;
        int c = 0;
        while (found == false && c < (rows * columns))
        {
            c++;
            x = UnityEngine.Random.Range(0, rows);
            y = UnityEngine.Random.Range(0, columns);
            
            if (map[x, y] < 2 && (x != 0 || y != 0))
            {
                found = true;
                map[x, y] = 3;
            }

        }
        return new Vector3(x * CellSize + CellSize / 2, y * CellSize + CellSize / 2);
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

        public void BuildWalls(GameObject topWallPrefab, GameObject topWallPrefabTorch, GameObject topWallPrefabMoving, GameObject rightWallPrefab, GameObject rightWallPrefabTorch, GameObject rightWallPrefabMoving, int [,] map, int rows, int columns) 
        {
            var x = this.i * cellSize;
            var y = this.j * cellSize;
            var hasTorchRight = UnityEngine.Random.Range(0f, 1f) < 0.18;
            var hasTorchTop = UnityEngine.Random.Range(0f, 1f) < 0.18;
            var canMoveRight = (i < rows-1) && UnityEngine.Random.Range(0f, 1f) < 0.4;
            var canMoveTop = (j < columns-1) && UnityEngine.Random.Range(0f, 1f) < 0.4;

            if (rightWall) 
            {
                var rightWall = Instantiate(hasTorchRight ? rightWallPrefabTorch :  (canMoveRight ? rightWallPrefabMoving  : rightWallPrefab));
                rightWall.transform.position = new Vector3(x + cellSize, y + (cellSize/2));
                if(hasTorchRight)
                    map[i, j] = 1;
            }

            if (topWall) 
            {
                var topWall = Instantiate(hasTorchTop ? topWallPrefabTorch : (canMoveTop ? topWallPrefabMoving : topWallPrefab));
                topWall.transform.position = new Vector3(x + (cellSize/2), y + cellSize);
                if (hasTorchTop)
                    map[i, j] = 1;
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
