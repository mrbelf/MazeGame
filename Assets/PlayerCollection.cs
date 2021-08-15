using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerCollection : MonoBehaviour
{
    private List<int> players = new List<int>();
    void Start()
    {
        AirConsole.instance.onConnect += OnDeviceConnect;
        AirConsole.instance.onDisconnect += OnDeviceDisconnect;
    }

    private void Update()
    {
        var plrs = AirConsole.instance.GetControllerDeviceIds();
        Debug.Log(plrs.Count);
    }

    private void OnDeviceConnect(int id)
    {
        players.Add(id);
    }

    private void OnDeviceDisconnect(int id)
    {
        players.Remove(id);
    }
    public List<int> GetPlayers()
    {
        return players;
    }
}
