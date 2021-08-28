using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class ConnectionManager2 : MonoBehaviour
{
    [SerializeField] private string [] colors;
    PlayerManager scoreManager;

    private void Start()
    {
        scoreManager = gameObject.GetComponent<PlayerManager>();
        AirConsole.instance.onConnect += OnDeviceConnect;
        AirConsole.instance.onDisconnect += OnDeviceDisconnect;
    }

    private void OnDeviceConnect(int id)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene" || scene.name == "ChoosePlayersScene")
        {

            AirConsole.instance.SetActivePlayers(4);
            var collection = AirConsole.instance.GetActivePlayerDeviceIds;
            foreach (int i in collection)
            {
                AirConsole.instance.Message(i, colors[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]);
            }
            scoreManager.AddPlayer(id);
        }
    }
    private void OnDeviceDisconnect(int id)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene" || scene.name == "ChoosePlayersScene")
        {
            AirConsole.instance.SetActivePlayers(4);
            var collection = AirConsole.instance.GetActivePlayerDeviceIds;
            foreach (int i in collection)
            {
                AirConsole.instance.Message(i, colors[AirConsole.instance.ConvertDeviceIdToPlayerNumber(i)]);
            }
        }
    }
}
