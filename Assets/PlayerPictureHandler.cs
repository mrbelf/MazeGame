using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPictureHandler : MonoBehaviour
{
    private bool locked = false;
    private int chosenSkin = 0;

    public Sprite[] skins;
    public GameObject skin;
    public GameObject lockedText;
    
    public void ChangeSkin(int change)
    {
        Debug.Log("change skin");
        if (locked == false)
        {

            Debug.Log("pre-chosenSkin");
            Debug.Log(chosenSkin);
            chosenSkin = (chosenSkin + change) % skins.Length;
            if(chosenSkin < 0)
            {
                chosenSkin = skins.Length - 1;
            }
            Debug.Log("change");
            Debug.Log(change);
            Debug.Log("Length");
            Debug.Log(skins.Length);
            Debug.Log("chosenSkin");
            Debug.Log(chosenSkin);
            skin.GetComponent<Image>().sprite = skins[chosenSkin];
        }
    }
    public void ChangeLock()
    {
        Debug.Log("change lock");
        if (locked == false)
        {
            locked = true;
            lockedText.GetComponent<Text>().text = "[                       ]";
        }
        else
        {
            locked = false;
            lockedText.GetComponent<Text>().text = "<                       >";
        }
    }

    public bool IsLocked()
    {
        return locked;
    }

}
