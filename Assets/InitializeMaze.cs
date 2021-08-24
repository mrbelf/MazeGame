using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class InitializeMaze : MonoBehaviour
{
    [SerializeField] public PlayerInitializer [] playerPrefab;
    [SerializeField] private Door doorPrefab;
    [SerializeField] private Key keyPrefab;
    int connections = 0;
    private MazeGenerator mazeGenerator;


    [SerializeField] private string[] colors;


    public static Color ColorFromString(string colorStr)
    {
        Color c = Color.black;

        if (colorStr.Equals("red"))
        {
            c = Color.red;
        }
        else if (colorStr.Equals("blue"))
        {
            c = Color.blue;
        }
        else if (colorStr.Equals("green"))
        {
            c = Color.green;
        }
        else if (colorStr.Equals("yellow"))
        {
            c = Color.yellow;
        }
        return c;
    }

    private void Start()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
        var maze = mazeGenerator.GenerateMaze();
        mazeGenerator.BuildMaze(maze);
        InitializeRound();
    }

    private void InitializeRound()
    {

        var count = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        for (int i = 0; i < count; i++)
        {
            Debug.Log("Initialized");
            Debug.Log(i);
            var color = ColorFromString(colors[i]);
            var player = Instantiate(playerPrefab[i]);
            var door = Instantiate(doorPrefab);
            var key = Instantiate(keyPrefab);

            player.GetComponent<PlayerInitializer>().Init(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), color);
            door.GetComponent<Door>().Init(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), color);
            key.GetComponent<Key>().Init(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), color);

            var positions = GetPositions(mazeGenerator.Dimentions, MazeGenerator.CellSize);

            player.transform.position = positions[0];
            door.transform.position = positions[1];
            key.transform.position = positions[2];
        }

    }

    /// <summary>
    /// Player -> Door -> Key
    /// </summary>
    /// <param name="currentConnection"></param>
    /// <returns></returns>
    private Vector3[] GetPositions(Vector2Int mazeDimentions, float cellSize)
    {
        
        var positions = new Vector3[3];
        positions[0] = new Vector3(cellSize / 2, cellSize / 2);
        positions[1] = mazeGenerator.RandomPositionInMazeBoundary();
        positions[2] = mazeGenerator.RandomPositionInMaze();
        return positions;
    }

    //private Vector3 RandomPositionInMazeDoor(Vector2Int mazeDimentions, float cellSize)
    //{
     //   return new Vector3(Random.Range(0, mazeDimentions.x) * cellSize + cellSize / 2, Random.Range(0, mazeDimentions.y) * cellSize + cellSize / 2);
    //}
}
