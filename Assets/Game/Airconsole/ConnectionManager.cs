using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private PlayerInitializer playerPrefab;



    private void Start()
    {
        AirConsole.instance.onConnect += OnDeviceConnect;
    }

    private void OnDeviceConnect(int id)
    {
        AirConsole.instance.Message(id, "red");
    }

    void Update()
    {
        
    }
}
