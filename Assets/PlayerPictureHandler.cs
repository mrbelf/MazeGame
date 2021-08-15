using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPictureHandler : MonoBehaviour
{
    public int playerNumber;
    private bool locked = false;
    private int chosenSkin = 0;
    public Sprite[] skins;
    public GameObject skin;
    public GameObject lockedText;
    public GameObject playerManager;
    private AirConsolePlayerChecker checker;
    private void Start()
    {
        checker = playerManager.GetComponent<AirConsolePlayerChecker>();
    }
    private void Update()
    {
        int temp = checker.GetSkinNumber(playerNumber);
        if(temp != chosenSkin)
        {
            chosenSkin = temp;
            skin.GetComponent<Image>().sprite = skins[chosenSkin];
        }
        bool temp2 = checker.GetLockedState(playerNumber);
        if (temp2 != locked)
        {
            locked = temp2;
            if (locked == true)
            {
                lockedText.GetComponent<Text>().text = "[                       ]";
            }
            else
            {
                lockedText.GetComponent<Text>().text = "<                       >";
            }
        }
    }
}
