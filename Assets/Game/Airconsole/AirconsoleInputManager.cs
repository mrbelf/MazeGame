using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirconsoleInputManager : MonoBehaviour, IGetInput
{
    private Vector2 input = Vector2.zero;

    [SerializeField] private int id = 0;

    public Vector2 GetAxis() => input;

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage; 
    }

    private void OnMessage(int deviceID, JToken data) 
    {
        if (id == deviceID && data["horizontal"] != null && data["vertical"] != null) 
        {
            input = new Vector2((float)data["horizontal"], (float)data["vertical"]).normalized;
        }
    }
}
