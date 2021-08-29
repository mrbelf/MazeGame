using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    private string state = "stopped";  // stopped, down, up
    public float speed = 0.005f;
    public float zLow = 1f;
    public float zHigh = 0f;
    private Collider2D coll;

    private void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }
    void Update()
    {
        if (state == "up")
        {
            MoveWall(true);
        }
        else if(state == "down")
        {
            MoveWall(false);
        }
    }

    private void MoveWall(bool up)
    {
        float z = transform.position.z;
        if (up)
        {
            if(z-speed < zHigh)
            {
                state = "stopped";
                coll.enabled = true;
            }
            else
            {
                transform.position += new Vector3(0, 0,-speed);
            }
        }
        else 
        {
            if (z + speed > zLow)
            {
                state = "stopped";
                coll.enabled = false;
            }
            else
            {
                transform.position += new Vector3(0, 0, speed);
            }
        }
    }

    public void LowerWall()
    {
        state= "down";
    }
    public void RiseWall()
    {
        state = "up";
    }

}
