using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject projector;
    public void Init(int id,Color c)
    {
        var manager = GetComponent<AirconsoleInputManager>();
        manager.SetId(id);
        int number = manager.GetPlayerNumber(id);
        int total = manager.GetTotalPlayers();
        GameObject obj = GameObject.Find("Camera" + (number+1).ToString());
        if (obj)
        {
            obj.GetComponent<FollowPlayer>().SetTransform(gameObject.GetComponent<Transform>(), number+1, total);

        }
        this.ChangeColor(projector.GetComponent<Projector>(), c);
        //projector.GetComponent<Projector>().material.SetColor("_Color", c);


        GetComponent<KeyOwner>().SetCode(id);
    }
    void ChangeColor(Projector projector, Color color)
    {
        var mat = new Material(projector.material);
        mat.name += "I";
        mat.SetColor("_Color", color);
        projector.material = mat;
    }
}
