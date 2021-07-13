using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] int code;

    public UnityEvent<int> OnWinEvent;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code)) 
        {
            Debug.Log("winner, winner, chicken dinner");
        }
    }

    private bool CodesMatch(KeyOwner owner, int code) 
    {
        return owner.GetKeyCode() == code && owner.ownerCode == code;
    }
}
