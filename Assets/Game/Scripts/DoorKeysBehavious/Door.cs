using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] int code;

    public int Id => code;

    public void Winner() 
    {
        gameObject.GetComponent<WinnerOnCollision>().CallWinner(code);
        AudioManager.GetInstance().PlayAudio("win");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code))
        {
            Winner();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code)) 
        {
            Winner();
        }
    }

    private bool CodesMatch(KeyOwner owner, int code) 
    {
        return owner.GetKeyCode() == code && owner.ownerCode == code;
    }

    public void Init(int id, Color c)
    {
        this.code = id;
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.color = c;
    }
}
