using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Key[] array = GameObject.FindObjectsOfType<Key>();
        int temp = array[0].code;
        Color cTemp = array[0].GetComponent<Renderer>().material.GetColor("_Color");

        array[0].GetComponent<Renderer>().material.SetColor("_Color", array[1].GetComponent<Renderer>().material.GetColor("_Color"));
        array[0].GetComponent<Key>().code = array[1].GetComponent<Key>().code;

        array[1].GetComponent<Renderer>().material.SetColor("_Color", cTemp);
        array[1].GetComponent<Key>().code = temp;
    }
}
