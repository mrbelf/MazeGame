using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private PlayerInitializer playerPrefab;
    [SerializeField] private Door doorPrefab;
    [SerializeField] private Key keyPrefab;
    int connections = 0;
    private MazeGenerator mazeGenerator;


    [SerializeField] private string [] colors;


    public static Color ColorFromString(string colorStr) 
    {
        Color c = Color.white;

        if (colorStr.Equals("red"))
        {
            c = Color.red;
        }
        else if (colorStr.Equals("blue"))
        {
            c = Color.blue;
        }
        else if (colorStr.Equals("black"))
        {
            c = Color.black;
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


        AirConsole.instance.onConnect += OnDeviceConnect;
    }

    private void OnDeviceConnect(int id)
    {
        var color = ColorFromString(colors[id]);

        var player = Instantiate(playerPrefab);
        var door = Instantiate(doorPrefab);
        var key = Instantiate(keyPrefab);

        player.GetComponent<PlayerInitializer>().Init(id, color);
        door.GetComponent<Door>().Init(id, color);
        key.GetComponent<Key>().Init(id, color);

        var positions = GetPositions(connections,mazeGenerator.Dimentions,MazeGenerator.CellSize);

        player.transform.position = positions[0];
        door.transform.position = positions[1];
        key.transform.position = positions[2];

        Debug.Log("Sending Message");
        AirConsole.instance.Message(id, colors[id]);
        connections++;
    }

    /// <summary>
    /// Player -> Door -> Key
    /// </summary>
    /// <param name="currentConnection"></param>
    /// <returns></returns>
    private Vector3[] GetPositions(int currentConnection, Vector2Int mazeDimentions, float cellSize) 
    {
        var positions = new Vector3[3];

        switch (currentConnection)
        {
            case 0:
                positions[0] = new Vector3(cellSize/2,cellSize/2);
                break;
            case 1:
                positions[0] = new Vector3(mazeDimentions.x * cellSize - cellSize/2, mazeDimentions.y * cellSize - cellSize/2);
                break;
            case 2:
                positions[0] = new Vector3(cellSize/2, mazeDimentions.y * cellSize - cellSize/2);
                break;
            case 3:
                positions[0] = new Vector3(mazeDimentions.x * cellSize - cellSize/2, cellSize/2);
                break;
            default:
                positions[0] = Vector3.zero;
                break;
        }

        positions[1] = RandomPositionInMaze(mazeDimentions,cellSize);
        positions[2] = RandomPositionInMaze(mazeDimentions,cellSize);
        return positions;
    }

    private Vector3 RandomPositionInMaze(Vector2Int mazeDimentions, float cellSize) 
    {
        return new Vector3(Random.Range(0,mazeDimentions.x)*cellSize + cellSize/2,Random.Range(0,mazeDimentions.y)*cellSize + cellSize/2);
    }
}
