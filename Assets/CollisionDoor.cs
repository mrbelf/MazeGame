using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDoor : MonoBehaviour
{
    public GameObject door;

    public void Winner()
    {
        door.GetComponent<Door>().Winner();
    }

    public int GetId()
    {
        return door.GetComponent<Door>().Id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.GetComponent<Door>().TriggerEnter2D(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        door.GetComponent<Door>().CollisionEnter2D(collision);
    }
}
