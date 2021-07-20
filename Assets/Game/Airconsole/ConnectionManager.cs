using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private PlayerInitializer playerPrefab;


    [SerializeField] private string [] colors;


    private void Start()
    {
        AirConsole.instance.onConnect += OnDeviceConnect;
    }

    private void OnDeviceConnect(int id)
    {
        var color = colors[id];
        Instantiate(playerPrefab).GetComponent<PlayerInitializer>().Init(id,color);

        AirConsole.instance.Message(id, color);
    }
}
