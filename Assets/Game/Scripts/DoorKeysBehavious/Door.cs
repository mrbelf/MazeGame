using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] int code;
    public GameObject[] lights;

    public int Id => code;

    public void Winner() 
    {
        gameObject.GetComponent<WinnerOnCollision>().CallWinner(code);
        AudioManager.GetInstance().PlayAudio("win");
    }

    public void TriggerEnter2D(Collider2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code))
        {
            Winner();
        }
    }

    public void CollisionEnter2D(Collision2D collision)
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
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().color = c;
        }
        
    }
    public void Start()
    {
        var transform = gameObject.GetComponent<Transform>();
        var x = transform.position.x;
        var y = transform.position.y;

        if (y < 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (y < 5)
        {
            if (x > 5)
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
    }
}
