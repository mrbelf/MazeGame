using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerSkinManager : MonoBehaviour
{
    public Sprite[] skins;
    void Start()
    {
        Image img = gameObject.GetComponent<Image>();
        PlayerManager manager = GameObject.Find("ConnectionManager").GetComponent<PlayerManager>();
        img.sprite = skins[manager.GetWinnerSkin()];
    }
}
