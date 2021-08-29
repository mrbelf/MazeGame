using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public GameObject button;
    public GameObject lights;
    private bool isCooldown = false;
    private float timeRemaining = 45;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCooldown)
        {
            isCooldown = true;
            timeRemaining = 45;
            SetButton(false);
            Key[] array = GameObject.FindObjectsOfType<Key>();
            if(array.Length == 2)
            {
                SwitchColors(array, 0, 1);
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    SwitchColors(array, i, (i + Random.Range(1, array.Length)) % array.Length);
                }
            }
        }
    }

    private void SwitchColors(Key [] array, int i, int j)
    {
        int temp = array[i].code;
        Color cTemp = array[i].GetComponent<Renderer>().material.GetColor("_Color");

        array[i].GetComponent<Renderer>().material.SetColor("_Color", array[j].GetComponent<Renderer>().material.GetColor("_Color"));
        array[i].GetComponent<Key>().code = array[j].GetComponent<Key>().code;

        array[j].GetComponent<Renderer>().material.SetColor("_Color", cTemp);
        array[j].GetComponent<Key>().code = temp;
    }

    private void Update()
    {
        if (isCooldown)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining < 0)
            {
                timeRemaining = 45;
                isCooldown = false;
                SetButton(true);
            }
        }
    }

    private void SetButton(bool on)
    {
        Transform tr = button.GetComponent<Transform>();
        tr.position += new Vector3(0f, 0f, on ? (-0.05f) : (0.05f));
        lights.SetActive(on); 
    }
}
