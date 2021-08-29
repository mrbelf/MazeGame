using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    public GameObject spotLight;
    private bool isCooldown = false;
    private float timeRemaining = 45;
    private float timeWall = 10;
    private bool wallDown = false;
    private WallMovement [] walls;
    private void Start()
    {
        walls = GameObject.FindObjectsOfType<WallMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCooldown)
        {
            isCooldown = true;
            timeRemaining = 45;
            SetLever(false);
            for(int i = 0; i < walls.Length; i++)
            {
                walls[i].GetComponent<WallMovement>().LowerWall();
            }
            timeWall = 10;
            wallDown = true;
        }
    }

    private void Update()
    {
        if (isCooldown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 45;
                isCooldown = false;
                SetLever(true);
            }
        }
        if (wallDown)
        {
            timeWall -= Time.deltaTime;
            if (timeWall < 0)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    walls[i].GetComponent<WallMovement>().RiseWall();
                }
                wallDown = false;
                timeWall = 10;
            }
        }
    }

    private void SetLever(bool on)
    {
        transform.eulerAngles = new Vector3(0, 0, on ? 0 : 180);
        spotLight.SetActive(on);
    }
}
