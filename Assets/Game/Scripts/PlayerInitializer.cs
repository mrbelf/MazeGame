using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public void Init(int id,Color c)
    {
        GetComponent<AirconsoleInputManager>().SetId(id);
        
        var renderer = GetComponent<SpriteRenderer>();
        
        if(renderer)
            renderer.color = c;
        
        GetComponent<KeyOwner>().SetCode(id);
    }
}
