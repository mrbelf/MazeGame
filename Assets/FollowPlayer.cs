using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform playerTransform;
    public void SetTransform(Transform t, int number, int total)
    {
        playerTransform = t;
        Camera cam = gameObject.GetComponent<Camera>();
        cam.enabled = true;
        if (total == 2)
        {
            if(number == 1)
            {
                cam.rect = new Rect(0f, 0.5f, 1f, 0.5f);
            }
            else
            {
                cam.rect = new Rect(0f, 0.0f, 1f, 0.5f);
            }
        }
        else if (total == 3)
        {
            if (number == 1)
            {
                cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            }
            else if (number == 2)
            {
                cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            }
            else
            {
                cam.rect = new Rect(0.25f, 0f, 0.5f, 0.5f);
            }
        }
        else if (total == 4)
        {
            if (number == 1)
            {
                cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            }
            else if (number == 2)
            {
                cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            }
            else if (number == 3)
            {
                cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            }
            else
            {
                cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
            }
        }
    }

    
    void Update()
    {
        if (playerTransform)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y-0.9f, playerTransform.position.z-1.4f);
        }
    }
}
