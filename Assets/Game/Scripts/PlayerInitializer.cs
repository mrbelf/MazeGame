using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public void Init(int id,Color c)
    {
        var manager = GetComponent<AirconsoleInputManager>();
        manager.SetId(id);
        int number = manager.GetPlayerNumber(id);
        int total = manager.GetTotalPlayers();
        Debug.Log("number");
        Debug.Log(number);
        GameObject obj = GameObject.Find("Camera" + (number+1).ToString());
        if (obj)
        {
            obj.GetComponent<FollowPlayer>().SetTransform(gameObject.GetComponent<Transform>(), number+1, total);

        }
        
        var renderer = GetComponent<SpriteRenderer>();
        
        if(renderer)
            renderer.color = c;
        
        GetComponent<KeyOwner>().SetCode(id);
    }
    
}
