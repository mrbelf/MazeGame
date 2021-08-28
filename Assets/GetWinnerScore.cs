using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GetWinnerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject [] players;
    public GameObject header;
    PlayerManager manager;
    void Start()
    {
        var size = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        manager = GameObject.Find("ConnectionManager").GetComponent<PlayerManager>();
        for (int i=0; i<4; i++)
        {
            if(i<size)
            {
                SetScore(i, manager.GetPlayerScore(i));
            }
            else
            {
                SetDisabled(i);
            }
        }
        SetWinner(manager.GetWinner());
    }

    void SetScore(int number, int score)
    {
        players[number].GetComponent<Text>().text = "Player " + (number+1).ToString() + "                " + score.ToString();
    }

    void SetDisabled(int number)
    {
        players[number].GetComponent<Text>().enabled = false;
    }

    void SetWinner(int number)
    {
        header.GetComponent<Text>().text = "Player " + (number + 1).ToString() + " WON!";
    }

    // Update is called once per frame
}
