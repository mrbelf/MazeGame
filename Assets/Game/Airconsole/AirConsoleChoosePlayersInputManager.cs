using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleChoosePlayersInputManager : MonoBehaviour
{
    private AirConsolePlayerChecker playerChecker;
    private void Awake()
    {
        playerChecker = gameObject.GetComponent<AirConsolePlayerChecker>();
        AirConsole.instance.onMessage += OnMessage;
    }

    private void OnMessage(int deviceID, JToken data)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "ChoosePlayersScene")
        {
            Debug.Log("message");
            
                Debug.Log("not null");
                if (data["horizontal"] != null && data["vertical"] != null)
                {
                    Debug.Log("comm1");
                    if ((float)data["horizontal"] > 0)
                    {
                        playerChecker.ChangeSkin(deviceID, - 1);
                    }
                    else if ((float)data["horizontal"] < 0)
                    {
                        playerChecker.ChangeSkin(deviceID, 1);
                    }
                }
                else if (data["action"] != null)
                {
                    Debug.Log("comm2");
                    if ((bool)data["action"] == true)
                    {
                        playerChecker.ChangeLocked(deviceID);
                    }
                }
            
        }
    }
}
