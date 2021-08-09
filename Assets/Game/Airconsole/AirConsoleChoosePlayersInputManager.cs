using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleChoosePlayersInputManager : MonoBehaviour
{
    private AirConsolePlayerChecker playerChecker;
    private PlayerPictureHandler pictureHandler;
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
            var obj = playerChecker.GetPlayerPictureGameObject(deviceID);
            if (obj != null)
            {
                Debug.Log("not null");
                pictureHandler = obj.GetComponent<PlayerPictureHandler>();
                if (data["horizontal"] != null && data["vertical"] != null)
                {
                    Debug.Log("comm1");
                    if ((float)data["horizontal"] > 0)
                    {
                        pictureHandler.ChangeSkin(-1);
                    }
                    else if ((float)data["horizontal"] < 0)
                    {
                        pictureHandler.ChangeSkin(1);
                    }
                }
                else if (data["action"] != null)
                {
                    Debug.Log("comm2");
                    if ((bool)data["action"] == true)
                    {
                        pictureHandler.ChangeLock();
                    }
                }
            }
        }
    }
}
