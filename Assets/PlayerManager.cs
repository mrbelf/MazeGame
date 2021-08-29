using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;


public class PlayerManager : MonoBehaviour
{
    Dictionary<int, int> scoreBoard = new Dictionary<int, int>();
    int[] skins = new int[4];
    int winner = 0;
    public void SetWinner(int id)
    {
        winner = AirConsole.instance.ConvertDeviceIdToPlayerNumber(id);
        scoreBoard.TryGetValue(id, out int score);
        scoreBoard[id] = score + 1;
    }
    public void SetSkin(int skinId, int playerNumber)
    {
        skins[playerNumber] = skinId;
    }
    public int GetSkin(int playerNumber)
    {
        return skins[playerNumber];
    }
    public int GetWinner()
    {
        return winner;
    }
    public int GetWinnerSkin()
    {
        return skins[winner];
    }
    public void AddPlayer(int id)
    {
        scoreBoard[id] = 0;
    }

    public int GetPlayerScore(int number)
    {
        int id = AirConsole.instance.ConvertPlayerNumberToDeviceId(number);
        scoreBoard.TryGetValue(id, out int score);
        return score;
    }
}
