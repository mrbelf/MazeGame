using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerOnCollision : MonoBehaviour
{
    GameObject conManager;
    ScoreManager scoManager;

    private void Start()
    {
        conManager = GameObject.Find("ConnectionManager");
        scoManager = conManager.GetComponent<ScoreManager>();
    }

    public void CallWinner(int id)
    {
        Debug.Log("Winner: ");
        Debug.Log(id);
        scoManager.SetWinner(id);
        SceneManager.LoadScene(sceneName: "WinnerScene");
    }
 }
