using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public void Init(int id,string colorStr)
    {
        Color c = Color.white;

        if (colorStr.Equals("red"))
        {
            c = Color.red;
        }
        else if (colorStr.Equals("blue"))
        {
            c = Color.blue;
        }
        else if (colorStr.Equals("black"))
        {
            c = Color.black;
        }
        else if (colorStr.Equals("yellow"))
        {
            c = Color.yellow;
        }

        GetComponent<AirconsoleInputManager>().SetId(id);
        GetComponent<SpriteRenderer>().color = c;
    }
}
