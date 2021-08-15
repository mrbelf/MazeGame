using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class ConnectionManager2 : MonoBehaviour
{
    [SerializeField] private PlayerInitializer playerPrefab;
    [SerializeField] private Door doorPrefab;
    [SerializeField] private Key keyPrefab;
    int connections = 0;
    private MazeGenerator mazeGenerator;


    [SerializeField] private string [] colors;


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


        AirConsole.instance.onConnect += OnDeviceConnect;
        AirConsole.instance.onDisconnect += OnDeviceDisconnect;
    }

    private void OnDeviceConnect(int id)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene" || scene.name == "ChoosePlayersScene")
        {

            AirConsole.instance.SetActivePlayers(4);
            int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(id);
            Debug.Log(playerNumber);
            Debug.Log("Sending Message");
            var collection = AirConsole.instance.GetActivePlayerDeviceIds;
            connections = collection.Count;
            foreach (int i in collection)
            {
                AirConsole.instance.Message(i, colors[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]);
            }
        }
    }
    private void OnDeviceDisconnect(int id)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene" || scene.name == "ChoosePlayersScene")
        {
            AirConsole.instance.SetActivePlayers(4);
            var collection = AirConsole.instance.GetActivePlayerDeviceIds;
            connections = collection.Count;
            foreach (int i in collection)
            {
                AirConsole.instance.Message(i, colors[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]);
            }
        }
    }
    public void InitializeRound()
    {

        var maze = mazeGenerator.GenerateMaze();
        mazeGenerator.BuildMaze(maze);
        var count = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        for (int i =0; i<count; i++)
        {
            Debug.Log("Initialized");
            Debug.Log(i);
            var color = ColorFromString(colors[i]);
            var player = Instantiate(playerPrefab);
            var door = Instantiate(doorPrefab);
            var key = Instantiate(keyPrefab);

            player.GetComponent<PlayerInitializer>().Init(i, color);
            door.GetComponent<Door>().Init(i, color);
            key.GetComponent<Key>().Init(i, color);

            var positions = GetPositions(connections, mazeGenerator.Dimentions, MazeGenerator.CellSize);

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
