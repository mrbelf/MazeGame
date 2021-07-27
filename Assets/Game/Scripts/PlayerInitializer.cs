using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public void Init(int id,Color c)
    {
        GetComponent<AirconsoleInputManager>().SetId(id);
        GetComponent<SpriteRenderer>().color = c;
    }
}
