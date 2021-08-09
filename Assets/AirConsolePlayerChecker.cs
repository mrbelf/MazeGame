using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsolePlayerChecker : MonoBehaviour
{
    public GameObject [] PlayerPicture;
    private GameObject obj;
    private PlayerCollection players;
    private PlayerPictureHandler pictureHandler;
    private 
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("PlayerCollection");
        players = obj.GetComponent<PlayerCollection>();

    }

    // Update is called once per frame
    void Update()
    {
        var ready = true;
        var collection = players.GetPlayers();
        for(int i=1; i<=4; i++)
        {
            if(collection.Contains(i))
            {
                PlayerPicture[i].SetActive(true);
                pictureHandler = PlayerPicture[i].GetComponent<PlayerPictureHandler>();
                if(pictureHandler.IsLocked() == false)
                {
                    ready = false;
                }
            }
            else
            {
                PlayerPicture[i].SetActive(false);
            }
        }
        if(ready == true && collection.Count > 0)
        {
            SceneManager.LoadScene(sceneName: "WinnerScene");
        }
    }
    public GameObject GetPlayerPictureGameObject(int id)
    {
        Debug.Log("call");
        var collection = players.GetPlayers();
        if (collection.Contains(id))
            return PlayerPicture[id];
        return null;

    }
}
