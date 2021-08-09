using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleStartInputManager : MonoBehaviour
{
    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    private void OnMessage(int deviceID, JToken data)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene")
        {
            if(data["action"] != null)
            {
                if ((bool)data["action"] == true)
                {
                    SceneManager.LoadScene(sceneName: "ChoosePlayersScene");
                }
            }
        }
    }
}
